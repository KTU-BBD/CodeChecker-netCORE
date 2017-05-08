using System;
using System.Diagnostics;
using System.Linq;
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

            foreach (var assignmentInput in assignment.Inputs)
            {
                var submission = new Submission()
                {
                    AssignmentId = codeAssignment.AssignmentId,
                    Code = codeAssignment.AssignmentSubmit.Code,
                    Language = codeAssignment.AssignmentSubmit.Language,
                    UserId = codeAssignment.SubmiterId
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
                        break;
                    }
                    else if (results.TimeSpent > assignment.TimeLimit)
                    {
                        submission.Verdict = SubmissionVerdict.TimeOverflow;
                        solvedAll = false;
                        break;
                    }
                    else if (results.Verdict != "OK")
                    {
                        submission.Verdict = SubmissionVerdict.Error;
                        solvedAll = false;
                        break;
                    }
                }
                catch (Exception e)
                {
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

            _context.SaveChanges();

        }
    }
}