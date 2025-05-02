﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persention.Attributes
{
    public class CacheAttribute(int durationInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService =  context.HttpContext.RequestServices.GetRequiredService<IServicesManager>().CacheService;

            var cacheKey = GenerateCacheKey(context.HttpContext.Request);


            var result = await cacheService.GetCacheValueAsync(cacheKey);
            if(!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result
                };
                return; 
            }
            var contextResult =  await next.Invoke();
            if(contextResult.Result is OkObjectResult okObject)
            {
                await cacheService.SetCacheValueAsync(cacheKey, okObject.Value,TimeSpan.FromSeconds(durationInSec));
            }
        }



        private string GenerateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach(var item in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");

            }
            return key.ToString();

        }
    }
}
