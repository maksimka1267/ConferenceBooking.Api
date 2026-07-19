using ConferenceBooking.Api.Data;
using ConferenceBooking.Api.Mappings;
using ConferenceBooking.Api.Repository;
using ConferenceBooking.Api.Repository.Interfaces;
using ConferenceBooking.Api.Services;
using ConferenceBooking.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using FluentValidation.AspNetCore;
using ClickHouse.Client.ADO;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreSql")));

// ClickHouse
builder.Services.AddScoped<ClickHouseConnection>(_ =>
    new ClickHouseConnection(
        builder.Configuration.GetConnectionString("ClickHouse")));

// Controllers
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
//Repository
builder.Services.AddScoped<IConferenceHallRepository, ConferenceHallRepository>();
builder.Services.AddScoped<IAdditionalServiceRepository, AdditionalServiceRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingServiceRepository, BookingServiceRepository>();
builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();

//Service
builder.Services.AddScoped<IConferenceHallService, ConferenceHallService>();
builder.Services.AddScoped<IAdditionalServiceService, AdditionalServiceService>();
builder.Services.AddScoped<IBookingManagementService, BookingManagementService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});
MapsterConfig.RegisterMappings();
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();