using System;
using System.Threading;
using System.Threading.Tasks;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Services.CodeSubmit;

namespace CodeChecker.Tasks
{
    public class CodeTestTask
    {
        private ApplicationDbContext _context;
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
            foreach (var assignmentInput in codeAssignment.Assignment.Inputs)
            {
                var results = await _codeSubmit.SubmitCode(new CodeSubmitViewModel()
                {
                    code = codeAssignment.AssignmentSubmit.Code,
                    inputText = assignmentInput.Text,
                    language = codeAssignment.AssignmentSubmit.Language,
                    memoryLimit = codeAssignment.Assignment.MemoryLimit,
                    timeLimit = codeAssignment.Assignment.TimeLimit,
                });

                _context.Submissions.Add(new Submission()
                {
                    Assignment = codeAssignment.Assignment,
                    Code = codeAssignment.AssignmentSubmit.Code,
                    Language = codeAssignment.AssignmentSubmit.Language,
                    TimeMs = (int) (results.TimeSpent * 1000),
                    Verdict = results.Verdict,
                    User = codeAssignment.Submiter
                });

                if (!assignmentInput.Output.Text.Equals(results.Output) || results.Verdict != "YES" ||
                    results.TimeSpent > codeAssignment.Assignment.TimeLimit)
                {
                    break;
                }
            }
        }
    }
}