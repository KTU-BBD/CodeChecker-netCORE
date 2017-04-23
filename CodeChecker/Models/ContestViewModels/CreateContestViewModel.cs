using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeChecker.Models.Models;
using CodeChecker.Models.UserViewModels;

namespace CodeChecker.Models.ContestViewModels
{
    public class CreateContestViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? StartAt { get; set; }

        [Required]
        public DateTime? EndAt { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string Password { get; set; }

        public CreateContestViewModel()
        {
            StartAt = null;
            EndAt = null;
        }

        [Required]
        [MinLength(1)]
        public string Creator { get; set; }
    }
}