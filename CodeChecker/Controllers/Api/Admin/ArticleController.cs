

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
        private readonly ApplicationUserRepository _userRepo;

        public ArticleController(ArticleRepository articleRepo, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo)
        {
            _articleRepo = articleRepo;
            _userManager = userManager;
            _userRepo = userRepo;
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
                if (ModelState.IsValid)
                {
                    var article = _articleRepo.GetArticleFull(updatedArticle.Id);
                    if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                    {
                        var updated = Mapper.Map(updatedArticle, article);
                        updated.Status = updatedArticle.Status;
                        updated.UpdatedAt = DateTime.Now;
                        _articleRepo.Update(updated);
                        return Ok("Article updated");
                    }

                    if (User.IsInRole("Contributor") && article.Creator.Id == _userManager.GetUserId(User))
                    {
                        if (article.Status == ArticleStatus.Submited || article.Status == ArticleStatus.Published)
                        {
                            return BadRequest("You are not allowed to edit article after submission");
                        }
                        var updated = Mapper.Map(updatedArticle, article);
                        updated.Status = updatedArticle.Status;
                        _articleRepo.Update(updated);
                        return Ok("Article updated");
                    }
                }
                else {
                    return BadRequest("Bad article data");
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

        
        [HttpPost("{id}")]
        public IActionResult ChangeStatus(int id, [FromBody] ArticleStatus status)
        {
            try
            {
                var article = _articleRepo.Get(id);
                if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    article.Status = status;
                    _articleRepo.Update(article);
                    return Ok("Status changed");
                }
                if (User.IsInRole("Contributor") && article.Creator.Id == _userManager.GetUserId(User))
                {
                    if (article.Status == ArticleStatus.Submited || article.Status == ArticleStatus.Published)
                    {
                        return BadRequest("You are not allowed to change status after submission");
                    }
                    article.Status = status;
                    _articleRepo.Update(article);
                    return Ok("Status changed");
                }
                return BadRequest("Unauthorized");
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost("")]
        public IActionResult CreateArticle([FromBody]ArticleTitleViewModel model)
        {
            try
            {
                if (model.Title != null) {
                    var newArticle = new Article();

                    var assignedUser = _userRepo.GetById(_userManager.GetUserId(User));
                    newArticle.Creator = assignedUser;
                    newArticle.Title = model.Title;
                    _articleRepo.Insert(newArticle);
                    return Ok(newArticle.Id);
                }
                return BadRequest("The title of your article cannot be empty");
            }
            catch (Exception ex)
            {

                return BadRequest("Error");
            }
        }
    }
}