using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeChecker.Models.Models;
using CodeChecker.Models.UserViewModels;

namespace CodeChecker.Models.ContestViewModels
{
    public class ViewContestViewModel
    {
        public string Name { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsPublic { get; set; }
        public UserViewModel Creator { get; set; }
    }
}