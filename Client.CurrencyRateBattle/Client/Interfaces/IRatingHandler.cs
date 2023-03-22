namespace Client.Interfaces;

public interface IRatingHandler
{
    /// <summary>
    /// Tries to get top 100 rating by total played games and print it as a table
    /// </summary>
    void PrintRatingByTotalPlayed();
    /// <summary>
    /// Tries to get top 100 rating by total victories and print it as a table
    /// </summary>
    void PrintRatingByVictories();
    /// <summary>
    /// Tries to get top 100 rating by winrate (only after 10 games played) and print it as a table
    /// </summary>
    void PrintRatingByWinrate();

}
