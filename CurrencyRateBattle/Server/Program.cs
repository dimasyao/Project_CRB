using System.Globalization;
using DbProvider.Common;
using DbProvider.Common.Extantions;
using DbProvider.Interfaces;
using FluentValidation.AspNetCore;
using RoomManager;
using RoomManager.Common;
using RoomManager.Interfaces;
using Server.Common;
using Server.Interfaces;
using Server.Services;
using SharedModels.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CustomConfiguration>(builder.Configuration.GetSection("CustomConfiguration"));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(loggingBuilder =>
{
    _ = loggingBuilder.AddFile("app_{0:yyyy}-{0:MM}-{0:dd}_.log", fileLoggerOpts =>
    {
        fileLoggerOpts.FormatLogFileName = fName =>
        {
            return string.Format(CultureInfo.CurrentCulture, fName, DateTime.UtcNow);
        };

        fileLoggerOpts.MaxRollingFiles = 3;
        fileLoggerOpts.FileSizeLimitBytes = 200_000;
    });
});

builder.Services.AddFluentValidation(options =>
{
    options.AutomaticValidationEnabled = false;
    _ = options.RegisterValidatorsFromAssemblyContaining<UserRegistryValidator>();
    _ = options.RegisterValidatorsFromAssemblyContaining<BetValidator>();
});

builder.Services.AddDbManager(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IRoomProvider, RoomProvider>();
builder.Services.AddScoped<IDetermWinner, DetermWinner>();
builder.Services.AddScoped<IRateCurrency, RateCurrency>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IBetRegistryService, BetRegistryService>();
builder.Services.AddScoped<IDbWriter, DbWriter>();
builder.Services.AddScoped<IDataReader, DataReader>();
builder.Services.AddScoped<ISignInService, SignInService>();

builder.Services.AddHostedService<BackgroundWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
