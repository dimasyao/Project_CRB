using System.Text.Json;
using Client.Interfaces;
using Microsoft.Extensions.Logging;

namespace Client.Handlers;

public class CurrencyListUpdateHandler : ICurrencyListUpdateHandler
{
    private readonly IHttpClientWorker _httpClientWorker;
    private readonly ILogger<CurrencyListUpdateHandler> _logger;

    public CurrencyListUpdateHandler(IHttpClientWorker httpClientWorker, ILogger<CurrencyListUpdateHandler> logger)
    {
        _httpClientWorker = httpClientWorker;
        _logger = logger;
    }
    public List<string> GetCurrencyList()
    {
        var currencies = new List<string>();
        var responce = _httpClientWorker.GetCurrencyList().Content.ReadAsStringAsync().Result;

        if (responce != null && !string.IsNullOrWhiteSpace(responce))
        {
            currencies = JsonSerializer.Deserialize<List<string>>(responce);
        }

        _logger.LogInformation("{Time}: CurrencyListUpdateHandler.GetCurrencyList() got currency list", DateTime.Now.ToShortTimeString());

        return currencies!;
    }
}
