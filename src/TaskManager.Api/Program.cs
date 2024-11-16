using CustomExceptionMiddleware;
using TaskManager.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper();
builder.Services.AddSwagger();
builder.Services.AddVersioning();
builder.Services.AddValidations();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration, builder.Environment);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "/taskmanager/documentation/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/taskmanager/documentation/v1/swagger.json", "API V1"));
    app.UseDeveloperExceptionPage();
}

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();

public partial class Program
{ }