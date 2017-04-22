using AutoMapper;
using CodeChecker.Models;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeChecker.Controllers.Api.Front
{
    public class UserController : FrontBaseController
    {
        private ApplicationUserRepository _repository;

        public UserController( ApplicationUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{num}")]
        public IActionResult GetTopUsers(int num)
        {
            var personViews =
                Mapper.Map<IEnumerable<TopUserViewModel>>(_repository.GetTopUsers(num));
            return Ok(personViews);
        }
    }
}