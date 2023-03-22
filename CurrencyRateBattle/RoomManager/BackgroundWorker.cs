using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoomManager.Interfaces;
using SharedModels.Models;

namespace RoomManager;

public class BackgroundWorker : IHostedService, IDisposable
{
    private readonly ILogger<BackgroundWorker> _logger;
    private readonly List<string> _currencies;
    private readonly int _roomLifetimeMinutes;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private Timer? _timer;
    private IServiceScope? _serviceScope;

    public BackgroundWorker(ILogger<BackgroundWorker> logger, IServiceScopeFactory serviceScopeFactory, IOptions<CustomConfiguration> options)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        var configuration = options.Value;
        _currencies = configuration.Currencies!;
        _roomLifetimeMinutes = configuration.RoomLivetime;
    }

#pragma warning disable CA1816 // Методы Dispose должны вызывать SuppressFinalize
    public void Dispose()
#pragma warning restore CA1816 // Методы Dispose должны вызывать SuppressFinalize
    {
        _timer?.Dispose();
        _serviceScope?.Dispose();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _serviceScope = _serviceScopeFactory.CreateScope();

        var roomProvider = _serviceScope.ServiceProvider.GetService<IRoomProvider>();

        var now = DateTime.UtcNow;
        var oddHourOffset = 0;

        if (now.Hour % 2 == 1)
        {
            oddHourOffset = 60;
        }

        var addminutes = 120 - now.Minute - oddHourOffset;

        if (roomProvider != null)
        {
            await roomProvider.TerminateRooms(_currencies);
            await roomProvider.CreateNewRooms(_currencies, 1320 + addminutes);
        }

        _timer = new Timer(async callback =>
        {
            await ReplaceRoomsAsync(roomProvider!);
        }
        , null
        , TimeSpan.FromMinutes(addminutes)
        , TimeSpan.FromMinutes(_roomLifetimeMinutes));
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        Dispose();
        return Task.CompletedTask;
    }

    private async Task ReplaceRoomsAsync(IRoomProvider roomProvider)
    {
        await roomProvider.CreateNewRooms(_currencies, 1440);
        await roomProvider.TerminateRooms(_currencies);

        _logger.LogInformation("Rooms Replaced");
    }
}
