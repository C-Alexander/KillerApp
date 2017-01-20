using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Shadow_Arena.Contexts;
using Shadow_Arena.Controllers;
using Shadow_Arena.Models;
using Shadow_Arena.Services;
using Xunit;

namespace XTests
{
    public class PlayerControllerTests
    {
        [Fact(DisplayName = "Can create new players")]
        public void TestNewPlayerCreation()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();

            var c = new PlayerController(
    new AuthMessageSender(),
    new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()),
    new Hashing());

            c.CreatePlayer(new RegisterViewModel()
            {
                Password = "wdasffdsdsdf!231!*",
                Username = "TestUser123456",
                Email = "testmail@testmail.com"
            });
            Assert.True(c.GetPlayerByUserName("TestUser123456") != null);
            c.DeletePlayer(c.GetPlayerByUserName("TestUser123456").Id);
        }

        [Fact(DisplayName = "Can not create invalid players")]
        public void TestFalseNewPlayerCreation()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();

            var c = new PlayerController(
                new AuthMessageSender(),
                new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()),
                new Hashing());
            var userNameRandom = Guid.NewGuid().ToString();
            c.ModelState.TryAddModelError("xUnit", "NotValid");
            c.CreatePlayer(new RegisterViewModel()
            {
                Password = "teststuff",
                Username = userNameRandom,
                Email = "test@testmail.com"
            });
            Assert.True(c.GetPlayerByUserName(userNameRandom) == null);
        } 

        [Fact(DisplayName = "Can delete a player")]
        public void TestPlayerDeletion()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();

            var c = new PlayerController(
    new AuthMessageSender(),
    new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()),
    new Hashing());
            c.CreatePlayer(new RegisterViewModel()
            {
                Password = "wdasffdsdsdf!231!",
                Username = "TestUser123456",
                Email = "testmail@testmail.com"
            });
            Console.WriteLine("Checking if player exists");
            Assert.True(c.GetPlayerByUserName("TestUser123456") != null);
            Console.WriteLine("Player exists! Deleting..");
            c.DeletePlayer((c.GetPlayerByUserName("TestUser123456").Id));
            Console.WriteLine("Played deleted!!!");
            Assert.True(c.GetPlayerByUserName("TestUser123456") == null);
            Console.WriteLine("Checking");
        }

        [Theory(DisplayName = "Can not delete false players")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(282938290)] //lol imagine if this game got so many players it would actually reach this id. now this would be a funny bug.
        public void TestFalsePlayerDeletion(int id)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();

            var c = new PlayerController(
    new AuthMessageSender(),
    new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()), new Hashing());

            c.DeletePlayer(id);
        }
    }
}
