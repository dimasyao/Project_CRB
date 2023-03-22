using System.Text.Json;
using RoomManager.Interfaces;
using RoomManager.Objects;

namespace RoomManager.Common;

public class RateCurrency : IRateCurrency
{
    private readonly HttpClient _httpClient = new();
    private readonly string _url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";

    public bool GetRate(string currencyCode, out decimal currencyRate)
    {
        currencyRate = 0;

        try
        {
            var stream = _httpClient.GetStreamAsync(_url).Result;
            string? text;
            using (var sr = new StreamReader(stream))
            {
                text = sr.ReadToEnd();
            }
            var currencies = JsonSerializer.Deserialize<List<Currency>>(text);
            if (currencies != null)
            {
                currencyRate = currencies.FirstOrDefault(x => x.CC == currencyCode)!.Rate + RandomRate();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    //since the nbu data is updated once a day, we twist the current course
    private static decimal RandomRate()
    {
        var rnd = new Random(0);
        return rnd.Next(10) <= 5 ? rnd.Next(0, 100) / 100 : -rnd.Next(0, 100) / 100;
    }
}
