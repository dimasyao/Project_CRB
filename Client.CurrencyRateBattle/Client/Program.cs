using System.Globalization;
using Client.Common;
using Client.Common.Validator;
using Client.Handlers;
using Client.Interfaces;
using Client.Menu;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var serviceCollection = new ServiceCollection()
    .AddLogging(loggingBuilder =>
    {
        _ = loggingBuilder.AddFile("app_{0:yyyy}-{0:MM}-{0:dd}_.log", fileLoggerOpts =>
        {
            fileLoggerOpts.FormatLogFileName = fName =>
            {
                return string.Format(CultureInfo.InvariantCulture, fName, DateTime.UtcNow);
            };

            fileLoggerOpts.MaxRollingFiles = 3;
            fileLoggerOpts.FileSizeLimitBytes = 200_000;
        });

    })
    .AddValidatorsFromAssemblyContaining<LoginValidator>(ServiceLifetime.Singleton)
    .AddValidatorsFromAssemblyContaining<PassValidator>(ServiceLifetime.Singleton)
    .AddValidatorsFromAssemblyContaining<NameValidator>(ServiceLifetime.Singleton)
    .AddSingleton<StartMenu>()
    .AddSingleton<RatingMenu>()
    .AddSingleton<RoomsMenu>()
    .AddSingleton<MainMenu>()
    .AddSingleton<PersonalOfficeMenu>()
    .AddSingleton<IAddBetHandler, AddBetHandler>()
    .AddSingleton<IPersonalOfficeHandler, PersonalOfficeHandler>()
    .AddSingleton<IAutorizationOrRegistration, AutorizationOrRegistrationHandler>()
    .AddSingleton<IBettingRoomsHandler, BettingRoomsHandler>()
    .AddSingleton<IHttpClientWorker, HttpClientWorker>(x => new HttpClientWorker(config.GetConnectionString("ServerConnection")))
    .AddSingleton<IRatingHandler, RatingsHandler>()
    .AddSingleton<ICurrencyListUpdateHandler, CurrencyListUpdateHandler>()
    .AddSingleton<INotificationsHandler, NotificationsHandler>()
    .AddSingleton<IInputItems, InputItems>();

var app = serviceCollection.BuildServiceProvider();

app.GetRequiredService<StartMenu>().Menu();
