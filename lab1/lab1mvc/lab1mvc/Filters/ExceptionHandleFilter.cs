using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace lab1mvc.Filters
{
    public class ExceptionHandleFilter : Attribute, IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {

            Console.WriteLine($"[Exception] {context.Exception.Message}");


            ViewResult res = new ViewResult();
            res.ViewName = "Error";
            context.Result = res;
        }
    }
}
