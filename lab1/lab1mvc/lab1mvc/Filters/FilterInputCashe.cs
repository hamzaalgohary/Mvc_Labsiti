using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

using System.CodeDom;
using System.Text;

namespace lab1mvc.Filters
{
    public class FilterInputCashe : IAsyncActionFilter
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _duration = TimeSpan.FromSeconds(30);

        public FilterInputCashe(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string bodyText = "";
            context.HttpContext.Request.EnableBuffering();
            using (var reader = new StreamReader(
         context.HttpContext.Request.Body,
                    detectEncodingFromByteOrderMarks: false,
                         leaveOpen: true))
            {
                bodyText = await reader.ReadToEndAsync();
                context.HttpContext.Request.Body.Position = 0;
            }

            string key = $"{context.HttpContext.Request.Path}_{context.HttpContext.Request.QueryString}_{bodyText}";

            if (_cache.TryGetValue(key, out IActionResult cachedResult))
            {
                Console.WriteLine(">>> Returning cached INPUT result <<<");
                context.Result = cachedResult; //
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is IActionResult result)
            {
                _cache.Set(key, result, _duration);
                Console.WriteLine(">>> Cached new INPUT result <<<");
            }




        }
    }
}