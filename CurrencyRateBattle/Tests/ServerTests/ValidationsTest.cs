using System.Collections.Generic;
using FluentValidation.Results;
using NUnit.Framework;
using Server.Common;
using SharedModels.Models;

namespace Tests.ServerTests;

public class ValidationsTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ValidateInvalidUserTest()
    {
        // Arrange
        var users = new List<User>
        {
            null!,
            new User(),
            new User()
            {
                Login = "",
                Password = " "
            },
            new User()
            {
                Login = "login_one",
                Password = null
            },
            new User()
            {
                Login = "login_one",
                Password = "qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop"
            },
            new User()
            {
                Login = "login_one",
                Password = "pass123",
                Name = "_"
            },
            new User()
            {
                Login = "123456789qwertyuiopasdf",
                Password = "pass123",
            }
        };

        var validator = new UserRegistryValidator(GetMocks._dataReader);
        var results = new List<ValidationResult>();

        // Act
        foreach (var user in users)
        {
            results.Add(validator.Validate(user));
        }

        // Assert
        foreach (var result in results)
        {
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Errors.Count);
        }
    }

    [Test]
    public void ValidateInvalidNameUserTest()
    {
        // Arrange
        var userNames = new List<string>
        {
            null!,
            string.Empty,
            " ",
            "1",
            "1234567890_1234567890_1234567890_12345",
            "abc_",
            "123",
            "абв",
            "*&()"
        };

        var validator = new NameValidator();
        var results = new List<ValidationResult>();

        // Act
        foreach (var user in userNames)
        {
            results.Add(validator.Validate(user));
        }

        // Assert
        foreach (var result in results)
        {
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Errors.Count);
        }
    }

    [Test]
    public void ValidateInvalidBetTest()
    {
        // Arrange
        var bets = new List<Bet>
        {
            null!,
            new Bet(),
            new Bet()
            {
                Login = null!
            },
            new Bet()
            {
                Login = string.Empty
            },
            new Bet()
            {
                Login = "login_1",
                Currency = null!
            },
            new Bet()
            {
                Login = "login_one",
                Currency = null!
            },
            new Bet()
            {
                Login = "login_one",
                Currency = string.Empty
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "1234"
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "12"
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "ЮСД"
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "USD"
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "USD",
                Sum = 0
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "USD",
                Sum = -1
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "USD",
                Sum = 1
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "USD",
                Sum = 1,
                BetValue = 0
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "USD",
                Sum = 1,
                BetValue = -1
            },
            new Bet()
            {
                Login = "login_one",
                Currency = "USD",
                Sum = 100000000000,
                BetValue = 1
            },
        };

        var validator = new BetValidator(GetMocks._dataReader);
        var results = new List<ValidationResult>();

        // Act
        foreach (var bet in bets)
        {
            results.Add(validator.Validate(bet));
        }

        // Assert
        foreach (var result in results)
        {
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Errors.Count);
        }
    }
}
