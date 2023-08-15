using Core.Interfaces;
using Ecommerce_Angular_DotNetAPI.Errors;
using Ecommerce_Angular_DotNetAPI.Extensions;
using Ecommerce_Angular_DotNetAPI.Middleware;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("con"));
});

builder.Services.AddApplicationServices();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .WithOrigins("https://localhost:4200");
    });
});


// *** Configure() *** //

var app = builder.Build();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    await context.Database.MigrateAsync();
    await SeedDB.Initialize(context, loggerFactory);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occured during migration");
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
