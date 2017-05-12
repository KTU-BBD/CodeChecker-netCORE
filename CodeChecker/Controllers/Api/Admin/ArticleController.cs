

using AutoMapper;
using CodeChecker.Models.ArticleViewModel;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;



namespace CodeChecker.Controllers.Api.Admin
{
    public class ArticleController : AdminBaseController
    {
        private readonly ArticleRepository _articleRepo;
        private UserManager<ApplicationUser> _userManager;

        public ArticleController(ArticleRepository articleRepo, UserManager<ApplicationUser> userManager)
        {
            _articleRepo = articleRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] DataFilterViewModel filterData)
        {
            if ((User.IsInRole("Administrator") || User.IsInRole("Moderator")))
            {
                var articles = _articleRepo.GetPagedDataIncludeDeleted(filterData);
                if (articles != null)
                    return Ok(articles);
            }
            else
            {
                var userId = _userManager.GetUserId(User);
                var query = _articleRepo.QueryDeletedWithCreator().Where(c => c.Creator.Id == userId);

                var articles = _articleRepo.GetPagedData(query, filterData);

                if (articles != null)
                {
                    return Ok(articles);
                }
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetFull(long id)
        {
            try
            {
                var article = _articleRepo.GetArticleFull(id);
                if (User.IsInRole("Contributor") && article.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    
                    return Ok(article);
                }
                else
                {
                    return BadRequest("Unauthorized");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] EditArticlePostViewModel updatedArticle)
        {

            try
            {
                var article = _articleRepo.GetArticleFull(updatedArticle.Id);
                if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    var updated = Mapper.Map(updatedArticle, article);
                    updated.Status = updatedArticle.Status;
                    _articleRepo.Update(updated);
                    return Ok("Article updated");
                }

                    if (User.IsInRole("Contributor") && article.Creator.Id == _userManager.GetUserId(User))
                    {
                        if (article.Status == ArticleStatus.Submited || article.Status == ArticleStatus.Published) {
                            return BadRequest("You are not allowed to edit article after submission");
                        }
                        var updated = Mapper.Map(updatedArticle, article);
                        updated.Status = updatedArticle.Status;
                        _articleRepo.Update(updated);
                        return Ok("Article updated");
                    }

                return BadRequest("Unauthorized");
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }

        }

        [HttpPost("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            try
            {
                var article = _articleRepo.GetArticleFull(id);
                if (User.IsInRole("Contributor") && article.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    _articleRepo.Delete(article);
                    return Ok("Contest deleted");
                }
                else
                {
                    return BadRequest("Unauthorized");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost("{id}")]
        public IActionResult RecoverArticle(int id)
        {
            try
            {
                var contest = _articleRepo.GetArticleFullWithDeleted(id);
                if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    _articleRepo.Recover(contest);
                    _articleRepo.ResetStatus(contest);
                    return Ok("Contest recovered");
                }
                else
                {
                    return BadRequest("Unauthorized");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [Authorize("CanEditArticles")]
        [HttpPost("{id}")]
        public IActionResult ChangeStatus(int id, [FromBody] ArticleStatus status)
        {
            var article = _articleRepo.Get(id);
            article.Status = status;
            _articleRepo.Update(article);
            return Ok();
        }
    }
}