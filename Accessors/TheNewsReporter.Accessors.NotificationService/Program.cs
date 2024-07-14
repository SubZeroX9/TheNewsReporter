using Microsoft.OpenApi.Models;
using TheNewsReporter.Accessors.NotificationApiService.Models;
using TheNewsReporter.Accessors.NotificationApiService.Services;

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification Service", Version = "v1" });
    }
    );

builder.Services.Configure<MailGunApiSettings>(
    builder.Configuration.GetSection("MailGunApiSettings"));

builder.Services.AddHttpClient<MailgunNotificationService>();

builder.Services.AddScoped<MailgunNotificationService>();

builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification Service v1");
            options.RoutePrefix = string.Empty;
        }
        );
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
