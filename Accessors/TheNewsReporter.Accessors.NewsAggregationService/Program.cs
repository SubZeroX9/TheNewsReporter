using Microsoft.OpenApi.Models;
using TheNewsReporter.Accessors.NewsAggregationService.Models;
using TheNewsReporter.Accessors.NewsAggregationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddControllers();
builder.Services.AddHttpClient<NewsService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "News Aggregation Service", Version = "v1" });
    }
    );

builder.Services.Configure<NewsApiSettings>(
    builder.Configuration.GetSection("NewsApiSettings"));

builder.Services.AddScoped<NewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "News Aggregation Service v1");
            options.RoutePrefix = string.Empty;
        }
        );
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
