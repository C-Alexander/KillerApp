using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shadow_Arena.Contexts;
using Shadow_Arena.Data;
using Shadow_Arena.Enumerations;
using Shadow_Arena.Models;
using Shadow_Arena.Repositories;
using Shadow_Arena.Services;

namespace Shadow_Arena.Controllers
{
    //not exactly secure, normally you'd use Identity and an Authorize attribute. But this is mostly a POC and frankly, worrying about security in this is out of scope

    //alex said, right before some kid on infralab figured out the requests and deleted every player with Charles
    public class PlayerController : Controller
    {
        private ShadowBetaDbContext _shadowContext = new ShadowBetaDbContext();
        private IPlayerRepository _repository;
        private IEmailSender _emailSender;

        /// <summary>
        /// Instantiates the Playercontroller with a repo of choice. Use memory repository for unit testing
        /// </summary>
#pragma warning disable 1584,1711,1572,1581,1580
        /// <param name="repo">Repository to use</param>
#pragma warning restore 1584,1711,1572,1581,1580
        /// 
        /// 
        /// 
//        public PlayerController(IPlayerRepository repo)
//        {
//            repository = repo;
//        }
//This SHOULD work but, Asp.net core offers dependency injection etc. I feel this may cause bugs!!
        public PlayerController(IEmailSender emailSender, IDatabaseManager databaseManager)
        {
            _emailSender = emailSender;
            _repository = new PlayerRepository(new PlayerSqlContext(databaseManager));
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(ContextData.PlayerId.ToString()) != null)
            {
                if (
                    _shadowContext.PlayerSession.Any(
                        s =>
                            s.Playerid ==
                            Convert.ToInt32(HttpContext.Session.GetString(ContextData.PlayerId.ToString()))
                            && s.Sessionid == HttpContext.Session.GetString(ContextData.SessionId.ToString())))
                {
                    return View("../Game/Index");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeletePlayer(int playerId)
        {
          
            Player player = new Player();
            player.Id = playerId;
            _repository.Delete(player);
            return View("Index");
        }

        [HttpPost]
        // ReSharper disable once UnusedMember.Local
        private IActionResult UpdatePlayer(Player player)
        {
            if (ModelState.IsValid) { 
            _repository.Update(player);
               }
            return View("Index");
        }

        [HttpPost]
        public IActionResult CreatePlayer(RegisterViewModel player)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(new Player()
                {
                    PassWord = player.Password,
                    UserName = player.Username
                });
                _emailSender.SendEmailAsync(player.Email,
                    "Welcome to the Shadow World",
                    String.Format(
                        "Welcome to the shadow world! \n\n" +
                        " Your username is: {0} \n" +
                        " Your password is: {1}"
                        , player.Username, player.Password));

            }
            return View("../Game/Index");
        }

        public Player GetPlayerByUserName(string userName)
        {
            return _shadowContext.Player.First(p => p.UserName == userName);
        }
    }
}
