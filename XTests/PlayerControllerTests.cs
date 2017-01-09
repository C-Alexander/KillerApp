using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShadowArena.Models;
using Shadow_Arena.Controllers;
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

        [Fact(DisplayName = "Can delete a player")]
        public void TestPlayerDeletion()
        {
            var c = new PlayerController();
            Assert.True(c.GetPlayerByUserName("TestUser123456") != null);
            c.DeletePlayer((c.GetPlayerByUserName("TestUser123456").Id));
            Assert.True(c.GetPlayerByUserName("TestUser123456") == null);
        }

    }
}
