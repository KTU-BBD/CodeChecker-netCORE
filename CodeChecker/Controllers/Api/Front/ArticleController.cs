using System.Collections.Generic;
using AutoMapper;
using CodeChecker.Models.ArticleViewModel;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class ArticleController : FrontBaseController
    {
        private readonly ArticleRepository _articleRepo;
        private const int ArticlesPerPage = 5;

        public ArticleController(ArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        [HttpGet("{page}")]
        public IActionResult Page(int page)
        {
            return Ok(Mapper.Map<IEnumerable<ArticleListViewModel>>(
                _articleRepo.GetPaginatedByStatus(page, ArticlesPerPage, ArticleStatus.Published)));
        }

        [HttpGet("{articleId}")]
        public IActionResult Get(int articleId)
        {
            var article = _articleRepo.GetActiveById(articleId);

            if (article == null)
            {
                return BadRequest("Article does not exist");
            }
            return Ok(Mapper.Map<ArticleViewModel>(article));
        }

        [HttpGet]
        public IActionResult PageCount()
        {
            return Ok(new {articleCount = _articleRepo.GetCountByStatus(ArticleStatus.Published)});
        }
    }
}