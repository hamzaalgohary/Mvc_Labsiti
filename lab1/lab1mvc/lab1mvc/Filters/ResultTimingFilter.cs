using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace lab1mvc.Filters
{
    public class ResultTimingFilter : Attribute, IResultFilter
    {
        private Stopwatch stpw = new Stopwatch();

        public void OnResultExecuting(ResultExecutingContext context)
        {
            stpw = Stopwatch.StartNew();
            Console.WriteLine(">>> Result execution starting...");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            stpw.Stop();
            Console.WriteLine($">>> Result executed in {stpw.ElapsedMilliseconds} ms");
        }
    }
}
