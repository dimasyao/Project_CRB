using System.Text.Json;
using Client.Common;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;

namespace Client.Handlers;

public class NotificationsHandler : INotificationsHandler
{
    private readonly IHttpClientWorker _httpClientWorker;
    private readonly ILogger<CurrencyListUpdateHandler> _logger;
    public NotificationsHandler(IHttpClientWorker httpClientWorker, ILogger<CurrencyListUpdateHandler> logger)
    {
        _httpClientWorker = httpClientWorker;
        _logger = logger;
    }
    public void PrintNotification(string login)
    {
        var newNotificationsAsSting = _httpClientWorker.GetNewNotifications(login).Content.ReadAsStringAsync().Result;

        var notifications = new List<UserHistory>();

        if (!string.IsNullOrEmpty(newNotificationsAsSting))
        {
            notifications = JsonSerializer.Deserialize<List<UserHistory>>(newNotificationsAsSting);
        }

        UIMessanger.ViewUserNotifications(notifications!);

        _logger.LogInformation("{Time}: NotificationsHandler.PrintNotification() printed notifications for user {Login}", DateTime.Now.ToShortTimeString(), login);
    }
}
