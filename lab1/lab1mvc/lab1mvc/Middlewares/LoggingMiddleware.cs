using System.Diagnostics;

namespace lab1mvc.Middlewares

{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //_logger.LogInformation("➡️ Request received.");
            Console.WriteLine($"Request =>{context.Request.Path} , Method => {context.Request.Method}");

            await _next(context);
            Console.WriteLine($"Response => {context.Response.StatusCode}");

            //_logger.LogInformation("⬅️ Response sent.");
        }



    }
}



//lab5
//    public class LoggingMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<LoggingMiddleware> _logger;

//        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            //_logger.LogInformation("➡️ Request received.");
//            Console.WriteLine($"Request =>{context.Request.Path} , Method => {context.Request.Path}");

//            await _next(context);
//            Console.WriteLine($"Response => {context.Response.StatusCode}");

//            //_logger.LogInformation("⬅️ Response sent.");
//        }



//    }
//}



























//public class LoggingMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger<LoggingMiddleware> _logger;

//    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
//    {
//        _next = next;
//        _logger = logger;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//          // start timer to calculate a time proccsess
//        var stopwatch = Stopwatch.StartNew();

//        
//         // sign in history request
//        _logger.LogInformation("➡️ Incoming Request: {method} {url}",
//            context.Request.Method,
//            context.Request.Path);

//        await _next(context); //  next middleware

//        stopwatch.Stop();

//         //after  do it , sign in time do and response
//        _logger.LogInformation("⬅️ Response: {statusCode} - Time: {elapsed} ms",
//            context.Response.StatusCode,
//            stopwatch.ElapsedMilliseconds);
//    }
//}