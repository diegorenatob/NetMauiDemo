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

            // --- 1) Configurar el DbContext para SQLite
            builder.Services.AddDbContext<MyBlogDbContext>(options =>
            {
                try
                {
                    // Obtenemos la ruta base
                    var dbDirectory = FileSystem.AppDataDirectory;

                    // Verificamos que el directorio exista; si no, lo creamos
                    if (!Directory.Exists(dbDirectory))
                    {
                        Directory.CreateDirectory(dbDirectory);
                        Console.WriteLine($"[DEBUG] Created directory: {dbDirectory}");
                    }

                    // Armamos la ruta completa del archivo .db3
                    var dbPath = Path.Combine(dbDirectory, "myblog.db3");
                    Console.WriteLine($"[DEBUG] Using database path: {dbPath}");

                    // Configuramos EF Core para apuntar a esa ruta
                    options.UseSqlite($"Data Source={dbPath}");
                }
                catch (Exception ex)
                {
                    // Aquí podrías hacer más que un Console.WriteLine:
                    // logs, telemetría, reintentos, etc.
                    Console.WriteLine($"[ERROR] Failed to configure DbContext: {ex.Message}");
                }
            });

            // --- 2) Repositorios
            builder.Services.AddTransient<IPostRepository, PostRepository>();

            // --- 3) HttpClient + servicio externo
            builder.Services.AddHttpClient<IExternalPostService, JsonPlaceholderClient>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            });


            // --- 4) Servicios (Application layer)
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IConnectivityService, MauiConnectivityService>();


            // --- 5) ViewModels
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<PostDetailViewModel>();
            builder.Services.AddTransient<PostDetailPage>();


            // --- 6) Pages
            builder.Services.AddTransient<MainPage>();

            // --- 7) Construimos la app
            var app = builder.Build();

            // --- 8) Aplicamos migraciones dentro de un scope
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
                    // Manejo de error:
                    // - Logs
                    // - Telemetry
                    // - Notificar al usuario
                    Console.WriteLine($"[ERROR] Error applying migrations: {ex}");
                }
            }

            return app;
        }
    }
}
