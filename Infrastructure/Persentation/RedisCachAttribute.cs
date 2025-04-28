using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
    public class RedisCachAttribute(int durationSec) : ActionFilterAttribute
    {
        public /*override*/ async Task OnActionExceutionAsync(ActionExecutedContext context, ActionExecutionDelegate next)
        {
            //1-
            var cachService = context.HttpContext.RequestServices
                .GetRequiredService<IServiceManager>().CachService;
            //2-
            string CachKey = GenerateCachKey(context.HttpContext.Request);
            //3-
            var Result = await cachService.GetCachValueAsync(CachKey);
            if (Result != null)
            {
                context.Result = new ContentResult
                {
                    Content = Result,
                    ContentType = "Application/Json",
                    StatusCode = (int)HttpStatusCode.OK,

                };
                return;
            }
            var contextResult = await next.Invoke();
            if (contextResult.Result is OkObjectResult okObject)
            {
                await cachService.SetCachValueAsync(CachKey, okObject, TimeSpan.FromSeconds(durationSec));
            }

        }
        private string GenerateCachKey(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                KeyBuilder.Append($"{item.Key}--{item.Value}");
            }
            return KeyBuilder.ToString();
        }

    }
}
