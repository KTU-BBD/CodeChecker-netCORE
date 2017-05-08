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
        public string Password { get; set; }
    }
}