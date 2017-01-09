using Microsoft.AspNetCore.Mvc;
using Shadow_Arena.Controllers;
using Shadow_Arena.Models;
using Xunit;

namespace XTests
{
    public class PlayerControllerTests
    {
        //atm asp.net core does not support proper unit testing. This means I have toeither work heavily around its DI or compromise by not using memory contexts. Sigh.
        public PlayerControllerTests()
        {
        }
        /// <summary>
        /// welp, need I really change my connection string for this
        /// </summary>
        [Fact(DisplayName = "The index should return the login page when not logged in")]
        public void TestNotLoggedInPage()
        {
            var c = new PlayerController();
            Assert.True(((ViewResult) c.Index()).ViewName == "Index");
        }

        [Fact(DisplayName = "Can create new players")]
        public void TestNewPlayerCreation()
        {
            var c = new PlayerController();
            c.CreatePlayer(new Player()
            {
                Experience = 0,
                Level = 10,
                PassWord = "wdasffdsdsdf!231!*",
                UserName = "TestUser123456"
            });
            Assert.True(c.GetPlayerByUserName("TestUser123456") != null);
        }

        [Fact(DisplayName = "Can not create invalid players")]
        public void TestFalseNewPlayerCreation()
        {
            var c = new PlayerController();
            c.CreatePlayer(new Player()
            {
                Experience = 0,
                Level = 10,
                PassWord = "gg",
                UserName = "TestUser1234564"
            });
            Assert.True(c.GetPlayerByUserName("TestUser1234564") == null);
            c.CreatePlayer(new Player()
            {
                Experience = 0,
                Level = 1000,
                PassWord = "wdasffdsdsdf!231!*",
                UserName = "TestUser1234564"
            });
            Assert.True(c.GetPlayerByUserName("TestUser1234564") == null);
        } 

        [Fact(DisplayName = "Can delete a player")]
        public void TestPlayerDeletion()
        {
            var c = new PlayerController();
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
            var c = new PlayerController();
            c.DeletePlayer(id);
        }
    }
}
