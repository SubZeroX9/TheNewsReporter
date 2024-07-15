using Microsoft.OpenApi.Models;
using TheNewsReporter.Accessors.UserPreferencesService.Models;
using TheNewsReporter.Accessors.UserPreferencesService.Services;

namespace TheNewsReporter.Accessors.UserPreferencesService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddDapr();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "User Preferences Service", Version = "v1" });
            }
            );

        builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDatabase"));

        builder.Services.AddSingleton<DbContext>();

        builder.Services.AddScoped<UserPreferencesDbService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "User Preferences Service v1");
                    options.RoutePrefix = string.Empty;
                }
                );
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors("AllowAll");

        app.MapControllers();

        app.Run();

    }
}

