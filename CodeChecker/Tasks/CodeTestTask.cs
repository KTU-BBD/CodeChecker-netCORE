using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.Repositories;
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
            var assignment = _context.Assignments.Include(a => a.Contest).Include(a => a.Inputs).ThenInclude(o => o.Output).FirstOrDefault(a => a.Id == codeAssignment.AssignmentId);

            var submissionGroup = new SubmissionGroup();

            _context.SubmissionGroups.Add(submissionGroup);
            _context.SaveChanges();
            int testNumber = 1;
            foreach (var assignmentInput in assignment.Inputs)
            {
                var submission = new Submission()
                {
                    AssignmentId = codeAssignment.AssignmentId,
                    Code = codeAssignment.AssignmentSubmit.Code,
                    Language = codeAssignment.AssignmentSubmit.Language,
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

                    if (!assignmentInput.Output.Text.Equals(results.Output))
                    {
                        submission.Verdict = SubmissionVerdict.WrongAnser;
                        solvedAll = false;
                    }
                    else if (results.TimeSpent > assignment.TimeLimit)
                    {
                        submission.Verdict = SubmissionVerdict.TimeOverflow;
                        solvedAll = false;
                    }
                    else if (results.Verdict != "OK")
                    {
                        submission.Verdict = SubmissionVerdict.Error;
                        solvedAll = false;
                    }

                    if (!solvedAll)
                    {
                        submissionGroup.Verdict = submission.Verdict;
                        submissionGroup.Message = $"Failed on test {testNumber}";
                        break;
                    }
                    testNumber++;
                }
                catch (Exception e)
                {
                    submissionGroup.Verdict = SubmissionVerdict.ServerError;
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

            _context.Update(assignment.Contest);
            _context.Update(submissionGroup);

            _context.SaveChanges();

        }
    }
}