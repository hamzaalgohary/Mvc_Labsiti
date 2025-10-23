using Microsoft.AspNetCore.Http;
using System;
using Serilog;
using System.Threading.Tasks;
namespace lab1mvc.Middlewares
{
    public class GlobalExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled exception caught by GlobalExceptionHandleMiddleware");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    message = "⚠️ Something went wrong! Please try again later.",
                    error = ex.Message
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}