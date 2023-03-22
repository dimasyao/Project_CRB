namespace RoomManager.Interfaces;

public interface IRateCurrency
{
    /// <summary>
    /// Get curr rate by input code of curr
    /// </summary>
    /// <param name="currencyCode">input code</param>
    /// <param name="currencyRate">output rate</param>
    /// <returns>result of compare</returns>
    public bool GetRate(string currencyCode, out decimal currencyRate);
}
