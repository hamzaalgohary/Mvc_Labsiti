using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lab1mvc.Filters
{
    public class HeaderAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredWord;
        private readonly string _headerName;

        public HeaderAuthorizeFilter(string headerName, string requiredWord)
        {
            _headerName = headerName;
            _requiredWord = requiredWord;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            // Check if the header exists
            if (!context.HttpContext.Request.Headers.TryGetValue(_headerName, out var headerValue))
            {
                context.Result = new UnauthorizedObjectResult($"Missing header: {_headerName}");
                return;
            }

            // Check if the header contains the required word
            if (!headerValue.ToString().Contains(_requiredWord, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedObjectResult($"Invalid value for {_headerName}");
                return;
            }

            // ✅ Authorized
            Console.WriteLine(">>> Authorization Passed");
        }
    }
}
