using lab1mvc.context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace lab1mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 🧠 Configure Serilog logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // 🧩 Tell ASP.NET to use Serilog instead of default logging
            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseMiddleware<lab1mvc.Middlewares.LoggingMiddleware>();

            // 🧱 Use the custom Global Exception Handler middleware
            app.UseMiddleware<lab1mvc.Middlewares.GlobalExceptionHandleMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();

            //app.Use(async (context, next) =>

            //{

            //    Console.WriteLine("mid1");
            //    await next();
            //    Console.WriteLine("mid 1/2");

            //});
            //app.Run(async(context) => {
            //    Console.WriteLine("run");
            //});
            //app.Use(async (context, next) =>

            //{

            //    Console.WriteLine("mid2");
            //    await next();
            //    Console.WriteLine("mid 2/2");

            //});
            app.UseAuthorization();


            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}



//lab to 4
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            builder.Services.AddControllersWithViews();


//            // Register DbContext with Dependency Injection
//            //builder.Services.AddDbContext<dblab1>(options =>
//            //    options.UseSqlServer("Server=GOHARY\\SQLEXPRESS;Database=lab1mvc;Trusted_Connection=True;TrustServerCertificate=True"));


//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }
//            app.UseRouting();
//            app.UseHttpsRedirection();
//            app.UseStaticFiles();
//            app.UseAuthorization();
//            app.MapStaticAssets();
//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=index}/{id?}")
//                .WithStaticAssets();

//            app.Run();
//        }
//    }
//}
