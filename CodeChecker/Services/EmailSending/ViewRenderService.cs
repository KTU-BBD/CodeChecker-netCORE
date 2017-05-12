using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CodeChecker.Services.EmailSending
{
    public class ViewRenderService
    {
        readonly IRazorViewEngine _viewEngine;
        readonly IHttpContextAccessor _httpContextAccessor;

        public ViewRenderService(IRazorViewEngine viewEngine, IHttpContextAccessor httpContextAccessor)
        {
            _viewEngine = viewEngine;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Render(string viewPath, bool isHtml)
        {
            return Render(string.Empty, viewPath, isHtml);
        }

        public string Render<TModel>(TModel model, string viewPath, bool isHtml)
        {
            string mailViewPath;

            if (isHtml)
            {
                mailViewPath = viewPath + "/html.cshtml";
            }
            else
            {
                mailViewPath = viewPath + "/text.cshtml";
            }

            var viewEngineResult = _viewEngine.GetView("~/Views/Email/", mailViewPath, false);

            if (!viewEngineResult.Success)
            {
                throw new InvalidOperationException($"Couldn't find view {viewPath}");
            }

            var view = viewEngineResult.View;

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext
                {
                    HttpContext = _httpContextAccessor.HttpContext,
                    ViewData =
                        new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                            {Model = model},
                    Writer = output
                };

                view.RenderAsync(viewContext).GetAwaiter().GetResult();

                return output.ToString();
            }
        }
    }
}