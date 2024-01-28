using ApartmentPriceTracker.Api.Models;
using ApartmentPriceTracker.Api.Services;
using ApartmentPriceTracker.Api.Services.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("SqlServerConnection"))
    .AddScoped<IApartmentService, ApartmentService>()
    .AddScoped<IHtmlParserService, PrinzipHtmlParserService>()
    .AddSingleton<IWebDriver, ChromeDriver>()
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
