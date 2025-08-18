using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerAPI.DB;
using System.Globalization;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add localization
            var supportedCultures = new[]
            {
                new CultureInfo("ru-RU"),
                new CultureInfo("en-US")
            };
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ru-RU");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // Add context DB
            builder.Services.AddDbContext<MainContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("MySql"),
                new MySqlServerVersion(new Version(8, 4, 3))));

            // swagger
            var useSwagger = bool.TryParse(builder.Configuration["UseSwagger"], out bool resConfBool) && resConfBool;
            if (useSwagger)
            {
                builder.Services.AddOpenApiDocument();
            }

            builder.Services.AddControllers();

            var app = builder.Build();

            var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
            if (localizationOptions != null)
            {
                app.UseRequestLocalization(localizationOptions);
            }

            if (useSwagger)
            {
                app.UseOpenApi();
                app.UseSwaggerUi();
            }

            app.UseCors(policy => policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
