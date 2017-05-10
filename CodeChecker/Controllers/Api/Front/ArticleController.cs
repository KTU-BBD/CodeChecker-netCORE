using System.Collections.Generic;
using AutoMapper;
using CodeChecker.Models.ArticleViewModel;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class ArticleController: FrontBaseController
    {
        private readonly ArticleRepository _articleRepo;
        private const int ArticlesPerPage = 5;

        public ArticleController(ArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        [HttpGet("{page}")]
        public IActionResult Get(int page)
        {
            return Ok(Mapper.Map<IEnumerable<ArticleListViewModel>>(
                _articleRepo.GetPaginatedByStatus(page, ArticlesPerPage, ArticleStatus.Published)));
        }
    }
}