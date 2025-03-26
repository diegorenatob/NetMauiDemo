using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Http;
using MyBlog.Application.Interfaces;
using MyBlog.Application.Services;
using MyBlog.Domain.Interfaces;
using MyBlog.Infrastructure.Data;
using MyBlog.Infrastructure.Repositories;
using MyBlog.Infrastructure.ServicesExtern;
using MyBlog.Maui.Services;
using MyBlog.Maui.ViewModels;
using MyBlog.Maui.Views;

namespace MyBlog.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddDbContext<MyBlogDbContext>(options =>
            {
                try
                {
                    var dbDirectory = FileSystem.AppDataDirectory;

                    if (!Directory.Exists(dbDirectory))
                    {
                        Directory.CreateDirectory(dbDirectory);
                        Console.WriteLine($"[DEBUG] Created directory: {dbDirectory}");
                    }

                    var dbPath = Path.Combine(dbDirectory, "myblog.db3");
                    Console.WriteLine($"[DEBUG] Using database path: {dbPath}");

                    options.UseSqlite($"Data Source={dbPath}");
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine($"[ERROR] Failed to configure DbContext: {ex.Message}");
                }
            });

            //  Repositorios
            builder.Services.AddTransient<IPostRepository, PostRepository>();

            // Http Client
            builder.Services.AddHttpClient<IExternalPostService, JsonPlaceholderClient>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            });


            // Services
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IConnectivityService, MauiConnectivityService>();


            // ViewModels
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<PostDetailViewModel>();


            //  Pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<PostDetailPage>();

            var app = builder.Build();

            // migrations
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<MyBlogDbContext>();
                    db.Database.Migrate(); 
                    Console.WriteLine("[INFO] Database migrated or created successfully.");
                }
                catch (Exception ex)
                {
                 
                    Console.WriteLine($"[ERROR] Error applying migrations: {ex}");
                }
            }

            return app;
        }
    }
}
