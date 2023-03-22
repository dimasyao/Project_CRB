namespace Client.Interfaces;

public interface ICurrencyListUpdateHandler
{
    /// <summary>
    /// Updates currency list from server
    /// </summary>
    List<string> GetCurrencyList();
}
