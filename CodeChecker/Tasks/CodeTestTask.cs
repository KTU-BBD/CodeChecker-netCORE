using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Services.CodeSubmit;

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

        public void Run(CodeAssignmentViewModel codeAssignment)
        {
            Task.Factory.StartNew(() => RunTask(codeAssignment));
        }

        private async void RunTask(CodeAssignmentViewModel codeAssignment)
        {
            var solvedAll = true;

            foreach (var assignmentInput in codeAssignment.Assignment.Inputs)
            {
                var submission = new Submission()
                {
                    AssignmentId = codeAssignment.Assignment.Id,
                    Code = codeAssignment.AssignmentSubmit.Code,
                    Language = codeAssignment.AssignmentSubmit.Language,
                    UserId = codeAssignment.Submiter.Id
                };

                try
                {
                    var results = await _codeSubmit.SubmitCode(new CodeSubmitViewModel()
                    {
                        code = codeAssignment.AssignmentSubmit.Code,
                        inputText = assignmentInput.Text,
                        language = codeAssignment.AssignmentSubmit.Language,
                        memoryLimit = codeAssignment.Assignment.MemoryLimit,
                        timeLimit = codeAssignment.Assignment.TimeLimit,
                    });

                    submission.Verdict = SubmissionVerdict.Success;
                    submission.TimeMs = (int) (results.TimeSpent * 1000);

                    if (!assignmentInput.Output.Text.Equals(results.Output))
                    {
                        submission.Verdict = SubmissionVerdict.WrongAnser;
                        solvedAll = false;
                        break;
                    }
                    else if (results.TimeSpent > codeAssignment.Assignment.TimeLimit)
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
                    _context.Add(submission);
                    _context.SaveChanges();
                }
            }

            if (solvedAll)
            {
                codeAssignment.Assignment.SolvedCount++;
                codeAssignment.Contest.SuccessfulSubmit++;
                _context.Update(codeAssignment.Assignment);
            }
            else
            {
                codeAssignment.Contest.UnsuccessfulSubmit++;
            }

            _context.Update(codeAssignment.Contest);
            _context.SaveChanges();
        }
    }
}