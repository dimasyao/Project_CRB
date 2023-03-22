using DbProvider.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Common;
using Server.Controllers;
using Server.Services;
using SharedModels.Models;
using System;
using System.Collections.Generic;

namespace Tests.ServerTests;
internal static class GetMocks
{
    public static readonly SignInService SignIn;
    public static readonly RegisterService UserRegister;
    public static readonly BetRegistryService BetRegistry;

    public static readonly IDataReader _dataReader;

    private static readonly User _user = new()
    {
        Login = "login_one",
        Password = "pass123",
        Balance = 10,
        History = null,
        Name = "login_one",
        TotalGames = 0,
        Victories = 0
    };

    private static readonly List<Room> _room = new()
    {
        new()
        {
            Bets = null,
            Currency = "USD",
            Date = DateTime.Now + new TimeSpan(5, 0, 0),
            RoomId = "room1"
        },
        new()
        {
            Bets = null,
            Currency = "USD",
            Date = DateTime.Now - new TimeSpan(2, 0, 0),
            RoomId = "room2"
        }
    };

    static GetMocks()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        var factory = serviceProvider.GetService<ILoggerFactory>();

        var loggerController = factory!.CreateLogger<AuthorizationController>();

        var dbReaderMock = new Mock<IDataReader>();
        _ = dbReaderMock.Setup(r => r.IsUserExists("login_one")).Returns(true);
        _ = dbReaderMock.Setup(r => r.GetPasswordHash("login_one")).Returns(_user.Password);
        _ = dbReaderMock.Setup(r => r.GetUser("login_one")).Returns(_user);
        _ = dbReaderMock.Setup(r => r.GetRoom("room1")).Returns(_room[0]);
        _ = dbReaderMock.Setup(r => r.GetRoomsList()).Returns(_room);

        _dataReader = dbReaderMock.Object;

        var dbWriterMock = new Mock<IDbWriter>();

        var validatorUserRegistry = new UserRegistryValidator(dbReaderMock.Object);
        var validatorBet = new BetValidator(dbReaderMock.Object);

        SignIn = new SignInService(factory!.CreateLogger<SignInService>(), dbReaderMock.Object);
        UserRegister = new RegisterService(factory!.CreateLogger<RegisterService>(), dbWriterMock.Object, validatorUserRegistry);
        BetRegistry = new BetRegistryService(factory!.CreateLogger<BetRegistryService>(), dbReaderMock.Object, dbWriterMock.Object, validatorBet);
    }
}
