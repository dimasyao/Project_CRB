using System.Collections.Generic;
using FluentValidation.Results;
using NUnit.Framework;
using Server.Services;
using SharedModels.Models;

namespace Tests.ServerTests;

public class BetManagerTest
{
    private readonly BetRegistryService _betRegistryService = GetMocks.BetRegistry;

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ValidBet()
    {
        //Arrange
        var bet = new Bet()
        {
            Sum = 10,
            BetValue = 32.3m,
            Currency = "USD",
            Login = "login_one"
        };

        var roomId = "room1";

        //Act
        var result = _betRegistryService.AddBet(bet, roomId);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }

    [Test]
    public void IvalidDatas()
    {
        //Arrange
        var invalidBets = new List<Bet>()
        {
            null!,
            new Bet(),
            new Bet()
        {
            Sum = -1,
            Login = "login_one",
            BetValue = 1,
            Currency = "USD"
        },
            new Bet()
        {
            Sum = 1,
            Login = "login_one",
            BetValue = -1,
            Currency = "USD"
        },
            new Bet()
        {
            Sum = 1,
            Login = "login",
            BetValue = 1,
            Currency = "USD"
        },
            new Bet()
        {
            Sum = 1,
            Login = "login_one",
            BetValue = 1,
            Currency = "usd"
        },
            new Bet()
        {
            Sum = 1,
            Login = null,
            BetValue = 1,
            Currency = "USD"
        },
            new Bet()
        {
            Sum = 1,
            Login = "login_one",
            BetValue = 1,
            Currency = null
        }
        };

        var validBet = new Bet()
        {
            Sum = 10,
            BetValue = 32.3m,
            Currency = "USD",
            Login = "login_one"
        };

        var validRoomId = "room1";
        var invalidRoomId = "room";

        List<ValidationResult> results = new();

        //Act
        foreach (var invalidBet in invalidBets)
        {
            results.Add(_betRegistryService.AddBet(invalidBet, validRoomId));
        }

        results.Add(_betRegistryService.AddBet(validBet, invalidRoomId));

        //Assert
        foreach (var result in results)
        {
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
    }

    [Test]
    public void InvalidTimeAlive()
    {
        //Arrange
        var validBet = new Bet()
        {
            Sum = 10,
            BetValue = 32.3m,
            Currency = "USD",
            Login = "login_one"
        };

        //Act
        var result = _betRegistryService.AddBet(validBet, "room2");

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsValid);

    }

}
