using Microsoft.AspNetCore.Mvc;
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
        //atm asp.net core does not support proper unit testing. This means I have toeither work heavily around its DI or compromise by not using memory contexts. Sigh.
        /// <summary>
        /// welp, need I really change my connection string for this
        /// </summary>
        [Fact(DisplayName = "The index should return the login page when not logged in")]
        public void TestNotLoggedInPage()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            var c = new PlayerController(new AuthMessageSender(), new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()));
            Assert.True(((ViewResult) c.Index()).ViewName == "Index");
        }

        [Fact(DisplayName = "Can create new players")]
        public void TestNewPlayerCreation()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            var c = new PlayerController(new AuthMessageSender(), new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()));
            c.CreatePlayer(new RegisterViewModel()
            {
                Password = "wdasffdsdsdf!231!*",
                Username = "TestUser123456"
            });
            Assert.True(c.GetPlayerByUserName("TestUser123456") != null);
        }

        [Fact(DisplayName = "Can not create invalid players")]
        public void TestFalseNewPlayerCreation()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            var c = new PlayerController(new AuthMessageSender(), new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()));
            c.CreatePlayer(new RegisterViewModel()
            {
                Password = "gg",
                Username = "TestUser1234564"
            });
            Assert.True(c.GetPlayerByUserName("TestUser1234564") == null);
        } 

        [Fact(DisplayName = "Can delete a player")]
        public void TestPlayerDeletion()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            var c = new PlayerController(new AuthMessageSender(), new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()));
            Assert.True(c.GetPlayerByUserName("TestUser123456") != null);
            c.DeletePlayer((c.GetPlayerByUserName("TestUser123456").Id));
            Assert.True(c.GetPlayerByUserName("TestUser123456") == null);
        }

        [Theory(DisplayName = "Can not delete false players")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(282939348294829)] //lol imagine if this game got so many players it would actually reach this id. now this would be a funny bug.
        public void TestFalsePlayerDeletion(int id)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            var c = new PlayerController(new AuthMessageSender(), new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>()));
            c.DeletePlayer(id);
        }
    }
}
