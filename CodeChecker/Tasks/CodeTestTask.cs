using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Services.CodeSubmit;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Tasks
{
    public class CodeTestTask
    {
        private readonly ApplicationDbContext _context;
        private readonly CodeSubmitService _codeSubmit;

        public CodeTestTask(ApplicationDbContext context, CodeSubmitService codeSubmit)
        {
            _context = context;
            _codeSubmit = codeSubmit;
        }

        public async void Run(CodeAssignmentViewModel codeAssignment)
        {
            await RunTask(codeAssignment);
        }

        public async Task RunTask(CodeAssignmentViewModel codeAssignment)
        {
            var solvedAll = true;
            var assignment = _context.Assignments.Include(a => a.Contest)
                .Include(a => a.Inputs)
                .ThenInclude(o => o.Output)
                .FirstOrDefault(a => a.Id == codeAssignment.AssignmentId);

            var submiteeUser = _context.Users.FirstOrDefault(u => u.Id == codeAssignment.SubmiterId);

            if (submiteeUser == null || assignment == null)
            {
                return;
            }

            var submissionGroup = new SubmissionGroup();

            _context.SubmissionGroups.Add(submissionGroup);
            _context.SaveChanges();
            int testNumber = 1;
            foreach (var assignmentInput in assignment.Inputs)
            {
                var submission = new Submission()
                {
                    AssignmentId = codeAssignment.AssignmentId,
                    UserId = codeAssignment.SubmiterId,
                    SubmissionGroup = submissionGroup,
                };

                try
                {
                    var results = await _codeSubmit.SubmitCode(new CodeSubmitViewModel()
                    {
                        code = codeAssignment.AssignmentSubmit.Code,
                        inputText = assignmentInput.Text,
                        language = codeAssignment.AssignmentSubmit.Language,
                        memoryLimit = assignment.MemoryLimit,
                        timeLimit = assignment.TimeLimit,
                    });

                    submission.Verdict = SubmissionVerdict.Success;
                    submission.TimeMs = (int) (results.TimeSpent * 1000);
                    submission.Output = results.Output;
                    submission.Memory = results.Memory;
                    submissionGroup.Memory = submission.Memory;
                    submissionGroup.Code = codeAssignment.AssignmentSubmit.Code;
                    submissionGroup.Assignment = assignment;
                    submissionGroup.Time = results.TimeSpent;
                    submissionGroup.Language = results.Language;
                    submissionGroup.Submitee = submiteeUser;

                    if (results.Verdict.Equals("COMPILATION_ERROR"))
                    {
                        submission.Verdict = SubmissionVerdict.CompilationError;
                        solvedAll = false;
                    }
                    else if (results.Verdict.Equals("RUNTIME_ERROR"))
                    {
                        submission.Verdict = SubmissionVerdict.RuntimeError;
                        solvedAll = false;
                    }
                    else if (results.Verdict.Equals("TIME_OVERFLOW"))
                    {
                        submission.Verdict = SubmissionVerdict.TimeOverflow;
                        solvedAll = false;
                    }
                    else if (results.Verdict.Equals("MEMORY_OVERFLOW"))
                    {
                        submission.Verdict = SubmissionVerdict.MemoryOverflow;
                        solvedAll = false;
                    }
                    else if (results.Verdict.Equals("OK") && !assignmentInput.Output.Text.Equals(results.Output))
                    {
                        submission.Verdict = SubmissionVerdict.WrongAnswer;
                        solvedAll = false;
                    }

                    if (!solvedAll)
                    {
                        submissionGroup.Verdict = submission.Verdict;
                        switch (submission.Verdict)
                        {
                            case SubmissionVerdict.CompilationError:
                            case SubmissionVerdict.RuntimeError:
                                submissionGroup.Message = submission.Output;
                                break;
                            default:
                                submissionGroup.Message = $"Failed on test {testNumber}";
                                break;
                        }
                        break;
                    }
                    testNumber++;
                }
                catch (Exception e)
                {
                    submissionGroup.Verdict = SubmissionVerdict.ServerError;
                    submissionGroup.Memory = 0;
                    submissionGroup.Message = $"Failed on test {testNumber}";
                    Debug.WriteLine("Error while trying to submit code: " + e.Message);
                    solvedAll = false;
                }
                finally
                {
                    await _context.AddAsync(submission);
                    await _context.SaveChangesAsync();
                }
            }

            if (solvedAll)
            {
                submissionGroup.Verdict = SubmissionVerdict.Success;
                submissionGroup.Message = "Accepted";

                assignment.SolvedCount++;
                assignment.Contest.SuccessfulSubmit++;
                _context.Update(assignment);
                Debug.WriteLine($"Solved assignment: {assignment.Id} {assignment.Name} successfully");
            }
            else
            {
                Debug.WriteLine($"Error while solving assignment: {assignment.Id} {assignment.Name}");
                assignment.Contest.UnsuccessfulSubmit++;
            }

            if (Math.Abs(submissionGroup.Time) < 0.02)
            {
                submissionGroup.Time = 0.01;
            }

            submissionGroup.Points = SubmissionPointCalculator(submissionGroup, submiteeUser);

            _context.Update(assignment.Contest);
            _context.Update(submissionGroup);

            _context.SaveChanges();

            assignment = null;
        }

        private long SubmissionPointCalculator(SubmissionGroup submission, ApplicationUser submitee)
        {
            if (submission.Assignment.Contest.IsContestEnded())
            {
                return 0;
            }

            if (submission.Assignment.Contest.StartAt > DateTime.Now &&
                submission.Assignment.Contest.EndAt < DateTime.Now)
            {
                return 0;
            }

            if (submission.Verdict == SubmissionVerdict.WrongAnswer)
            {
                return -40;
            }

            var contestLength = submission.Assignment.Contest.EndAt.Subtract(submission.Assignment.Contest.StartAt)
                .TotalSeconds;

            var timeLeft = submission.Assignment.Contest.EndAt.Subtract(DateTime.Now).TotalSeconds;


            var percentagePoints = timeLeft / contestLength;

            var points = (long) (submission.Assignment.MaxPoints * 0.2 +
                                 0.8 * percentagePoints * submission.Assignment.MaxPoints);

            var failedSubmitPoints = _context.SubmissionGroups.Where(s => s.Submitee.Id == submission.Submitee.Id &&
                                                                          s.Assignment.Id == submission.Assignment.Id &&
                                                                          s.Verdict == SubmissionVerdict.WrongAnswer)
                .Sum(s => s.Points);

            if (submission.Assignment.MaxPoints * 0.2 > failedSubmitPoints + points)
            {
                return (long) (submission.Assignment.MaxPoints * 0.2);
            }

            return (long) (submission.Assignment.MaxPoints * 0.2 +
                           0.8 * percentagePoints * submission.Assignment.MaxPoints) + failedSubmitPoints;
        }
    }
}