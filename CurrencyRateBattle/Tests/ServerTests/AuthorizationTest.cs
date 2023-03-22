using System.Collections.Generic;
using FluentValidation.Results;
using NUnit.Framework;
using Server.Interfaces;
using SharedModels.Models;

namespace Tests.ServerTests;

public class AuthorizationTest
{
    private readonly IRegisterService _registerService = GetMocks.UserRegister;
    private readonly ISignInService _signInService = GetMocks.SignIn;

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void SignInValidDataTest()
    {
        // Arrange
        var user = new User()
        {
            Login = "login_one",
            Password = "pass123"
        };

        // Act
        var result = _signInService!.IsValidLoginPass(user);

        // Assert
        Assert.IsNotNull(result);
        Assert.True(result);
    }

    [Test]
    public void SignInInvalidDataTest()
    {
        // Arrange
        var invalidUsers = new List<User>()
        {
            new User()
            {
                Login = "",
                Password = "pass123"
            },
            new User()
            {
                Login = "login_one",
                Password = ""
            },
            new User()
        };

        // Act
        var results = new List<bool>();

        foreach (var item in invalidUsers)
        {
            results.Add(_signInService!.IsValidLoginPass(item));
        }

        // Assert
        foreach (var result in results)
        {
            Assert.IsNotNull(result);
            Assert.False(result);
        }
    }

    [Test]
    public void RegistrationInvalidInputTest()
    {
        // Arrange
        var users = new List<User>
        {
            null!,
            new User(),
            new User()
            {
                Login = ""
            },
            new User()
            {
                Login = "123456789qwertyuiopasdf"
            }
        };

        var results = new List<ValidationResult>();

        // Act
        foreach (var user in users)
        {
            results.Add(_registerService.WriteNewUserAsync(user));
        }

        // Assert
        foreach (var result in results)
        {
            Assert.NotNull(result);
            Assert.Greater(result.Errors.Count, 0);
        }
    }
}
