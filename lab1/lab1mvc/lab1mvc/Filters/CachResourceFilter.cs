using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace lab1mvc.Filters
{
    public class CachResourceFilter : IResourceFilter
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(20);

        public CachResourceFilter(IMemoryCache cache)
        {
            _cache = cache;

        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var key = context.HttpContext.Request.Path.ToString();

            if (_cache.TryGetValue(key, out var cachedResult))
            {
                Console.WriteLine(">>> Returning cached result  <<<");
                context.Result = (IActionResult)cachedResult;
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (context.Result != null)
            {
                var key = context.HttpContext.Request.Path.ToString();
                _cache.Set(key, context.Result, _cacheDuration);
                Console.WriteLine(">>> Result cached for 20 seconds <<<");
            }
        }
    }
}
