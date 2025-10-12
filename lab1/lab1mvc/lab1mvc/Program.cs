using lab1mvc.context;
using Microsoft.EntityFrameworkCore;

namespace lab1mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            // Register DbContext with Dependency Injection
            //builder.Services.AddDbContext<dblab1>(options =>
            //    options.UseSqlServer("Server=GOHARY\\SQLEXPRESS;Database=lab1mvc;Trusted_Connection=True;TrustServerCertificate=True"));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
