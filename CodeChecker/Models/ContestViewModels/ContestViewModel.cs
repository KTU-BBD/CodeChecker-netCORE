using CodeChecker.Models.Models;
using CodeChecker.Models.UserViewModels;
using System;
using System.Collections.Generic;

namespace CodeChecker.Models.ContestViewModels
{
    public class ContestViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public double Length { get; set; }
        public UserViewModel Creator { get; set; }
        public bool Joined { get; set; }
        public bool IsPublic { get; set; }
        public bool IsStarted { get; set; }
    }
}