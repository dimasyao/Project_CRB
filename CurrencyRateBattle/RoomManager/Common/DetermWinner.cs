using DbProvider.Interfaces;
using RoomManager.Interfaces;
using SharedModels.Models;

namespace RoomManager.Common;

public class DetermWinner : IDetermWinner
{
    private readonly IDbWriter _dbWriter;
    private readonly IRateCurrency _rateCurrency;

    public DetermWinner(IDbWriter dbWriter, IRateCurrency rateCurrency)
    {
        _rateCurrency = rateCurrency;
        _dbWriter = dbWriter;
    }

    public void RoomWithApproximationAndAccurate(List<Bet> bets, string currencyCode)
    {
        if (_rateCurrency.GetRate(currencyCode, out var currentExchangeRate) || bets.Count < 2)
        {
            var accurateBetsWinners = new List<Bet>();
            var approximateBetsWinners = new List<Bet>();

            var amountWinningAccurateBets = 0m;
            var amountWinningApproximateBets = 0m;
            var countApproximateBetsWinners = 0;

            var pool = bets.Sum(x => x.Sum);
            var winners = new List<string>();

            foreach (var bet in bets)
            {
                if (bet.BetValue == currentExchangeRate)
                {
                    accurateBetsWinners.Add(bet);
                    amountWinningAccurateBets += bet.Sum;
                }
                else
                {
                    if (currentExchangeRate - bet.BetValue > 0)
                    {
                        approximateBetsWinners.Add(new Bet()
                        {
                            Login = bet.Login,
                            Sum = bet.Sum,
                            BetValue = currentExchangeRate - bet.BetValue,
                            Currency = bet.Currency
                        });
                    }
                    else
                    {
                        approximateBetsWinners.Add(new Bet()
                        {
                            Login = bet.Login,
                            Sum = bet.Sum,
                            BetValue = -(currentExchangeRate - bet.BetValue),
                            Currency = bet.Currency
                        });
                    }
                }
            }

            approximateBetsWinners = approximateBetsWinners.OrderBy(x => x.BetValue).ToList();

            countApproximateBetsWinners = (int)Math.Ceiling(approximateBetsWinners.Count * 0.3);

            if (accurateBetsWinners.Count != 0)
            {
                var poolAccurateBets = 0.6m * pool;
                pool -= poolAccurateBets;

                foreach (var winner in accurateBetsWinners)
                {
                    var amountToCharged = winner.Sum / amountWinningAccurateBets * poolAccurateBets;

                    winners.Add(winner.Login!);
                    AddWinToUserHistory(currencyCode, winner, amountToCharged);
                }
            }

            for (var i = 0; i < countApproximateBetsWinners; i++)
            {
                amountWinningApproximateBets += approximateBetsWinners[i].Sum;
            }

            for (var i = 0; i < countApproximateBetsWinners; i++)
            {
                var amountToCharged = approximateBetsWinners[i].Sum / amountWinningApproximateBets * pool;

                winners.Add(approximateBetsWinners[i].Login!);
                AddWinToUserHistory(currencyCode, bets.FirstOrDefault(x => x.Login == approximateBetsWinners[i].Login)!, amountToCharged);
            }

            foreach (var bet in bets)
            {
                if (!winners.Contains(bet.Login!))
                {
                    AddLoseToUserHistory(currencyCode, bet);
                }
            }
        }
        else
        {
            ReturnBets(bets);
        }
        _dbWriter.Update();
    }

    public void RoomWithAccurate(List<Bet> bets, string currencyCode)
    {
        if (_rateCurrency.GetRate(currencyCode, out var currentExchangeRate) || bets.Count < 2)
        {
            var winners = new List<string>();
            var accurateBetsWinners = new List<Bet>();
            var amountWinningAccurateBets = 0m;

            var pool = bets.Sum(x => x.Sum);

            foreach (var bet in bets)
            {
                if (bet.BetValue == currentExchangeRate)
                {
                    accurateBetsWinners.Add(bet);
                    amountWinningAccurateBets += bet.Sum;
                }
            }

            foreach (var winner in accurateBetsWinners)
            {
                var amountToCharged = winner.Sum / amountWinningAccurateBets * pool;
                winners.Add(winner.Login!);
                AddWinToUserHistory(currencyCode, winner, amountToCharged);
            }

            foreach (var bet in bets)
            {
                if (!winners.Contains(bet.Login!))
                {
                    AddLoseToUserHistory(currencyCode, bet);
                }
            }
        }
        else
        {
            ReturnBets(bets);
        }

        _dbWriter.Update();
    }

    public void RoomWithApproximation(List<Bet> bets, string currencyCode)
    {
        if (_rateCurrency.GetRate(currencyCode, out var currentExchangeRate) || bets.Count < 2)
        {
            var amountWinningApproximateBets = 0m;
            var countApproximateBetsWinners = 0;
            var winners = new List<string>();

            var approximateBetsWinners = new List<Bet>();
            var pool = bets.Sum(x => x.Sum);

            foreach (var bet in bets)
            {
                if (currentExchangeRate - bet.BetValue >= 0)
                {
                    approximateBetsWinners.Add(new Bet()
                    {
                        Login = bet.Login,
                        Sum = bet.Sum,
                        BetValue = currentExchangeRate - bet.BetValue,
                        Currency = bet.Currency
                    });
                }
                else
                {
                    approximateBetsWinners.Add(new Bet()
                    {
                        Login = bet.Login,
                        Sum = bet.Sum,
                        BetValue = -(currentExchangeRate - bet.BetValue),
                        Currency = bet.Currency
                    });
                }
            }

            approximateBetsWinners = approximateBetsWinners.OrderBy(x => x.BetValue).ToList();

            countApproximateBetsWinners = (int)Math.Ceiling(approximateBetsWinners.Count * 0.3);

            for (var i = 0; i < countApproximateBetsWinners; i++)
            {
                amountWinningApproximateBets += approximateBetsWinners[i].Sum;
            }

            for (var i = 0; i < countApproximateBetsWinners; i++)
            {
                var amountToCharged = approximateBetsWinners[i].Sum / amountWinningApproximateBets * pool;

                winners.Add(approximateBetsWinners[i].Login!);
                AddWinToUserHistory(currencyCode, bets.FirstOrDefault(x => x.Login == approximateBetsWinners[i].Login)!, amountToCharged);
            }

            foreach (var bet in bets)
            {
                if (!winners.Contains(bet.Login!))
                {
                    AddLoseToUserHistory(currencyCode, bet);
                }
            }
        }
        else
        {
            ReturnBets(bets);
        }

        _dbWriter.Update();
    }

    //add wins to user history in db
    private void AddWinToUserHistory(string currencyCode, Bet winner, decimal amountToCharged)
    {
        _dbWriter.AddBalance(winner.Login!, amountToCharged);
        _dbWriter.AddVictories(winner.Login!, 1);
        _dbWriter.AddGames(winner.Login!, 1);

        var userHistory = new UserHistory()
        {
            Date = DateTime.UtcNow,
            Currency = currencyCode,
            Prediction = winner.BetValue,
            AmountOfBet = winner.Sum,
            AmountOfWining = amountToCharged,
            HasWon = true
        };

        _dbWriter.AddUserHistory(userHistory, winner.Login!);
    }

    //ad lose to user history in db
    private void AddLoseToUserHistory(string currencyCode, Bet bet)
    {
        _dbWriter.AddGames(bet.Login!, 1);

        var userHistory = new UserHistory()
        {
            Date = DateTime.UtcNow,
            Currency = currencyCode,
            Prediction = bet.BetValue,
            AmountOfBet = bet.Sum,
            AmountOfWining = 0,
            HasWon = false
        };

        _dbWriter.AddUserHistory(userHistory, bet.Login!);
    }

    // return money to users, if the rate is not calculated
    private void ReturnBets(List<Bet> bets)
    {
        foreach (var bet in bets)
        {
            _dbWriter.AddBalance(bet.Login!, bet.Sum);
        }
    }
}
