using SharedModels.Models;

namespace RoomManager.Interfaces;

public interface IDetermWinner
{
    /// <summary>
    /// determines and distributes the winnings among the winners according to the rule
    /// "approximation and accurate"
    /// </summary>
    /// <param name="bets">List of bets in room</param>
    /// <param name="currencyCode">Cuurence code in room</param>
    public void RoomWithApproximationAndAccurate(List<Bet> bets, string currencyCode);

    /// <summary>
    /// determines and distributes the winnings among the winners according to the rule
    /// "accurate"
    /// </summary>
    /// <param name="bets">List of bets in room</param>
    /// <param name="currencyCode">Cuurence code in room</param>
    public void RoomWithAccurate(List<Bet> bets, string currencyCode);

    /// <summary>
    /// determines and distributes the winnings among the winners according to the rule
    /// "approximation"
    /// </summary>
    /// <param name="bets">List of bets in room</param>
    /// <param name="currencyCode">Cuurence code in room</param>
    public void RoomWithApproximation(List<Bet> bets, string currencyCode);
}
