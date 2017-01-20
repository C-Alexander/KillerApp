using System;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using Shadow_Arena.Contexts;
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
        private IPlayerRepository _repository;
        private IEmailSender _emailSender;
        private readonly IHashing _hashing;
        private LoginManager _loginManager;

        /// <summary>
        /// Instantiates the Playercontroller with a repo of choice. Use memory repository for unit testing
        /// </summary>
        /// <param name="emailSender">The email service to use</param>
        /// <param name="databaseManager">The database manager to use</param>
        /// <param name="hashing">The hashing service to use</param>
//        public PlayerController(IPlayerRepository repo)
//        {
//            repository = repo;
//        }
//This SHOULD work but, Asp.net core offers dependency injection etc. I feel this may cause bugs!!
        public PlayerController(IEmailSender emailSender, IDatabaseManager databaseManager, IHashing hashing)
        {
            _emailSender = emailSender;
            _hashing = hashing;
            //  _protector = provider.CreateProtector("PlayerController"); OK, the protector gives different values no matter what I do. New plan.
            _repository = new PlayerRepository(new PlayerSQLContext(databaseManager));
            _loginManager = new LoginManager(new PlayerRepository(new PlayerSQLContext(databaseManager)));
        }

        public IActionResult Index()
        {
            if (_loginManager.IsLoggedIn(HttpContext.Session))
            {
                return View("../Game/Index");
            }
            return RedirectToAction("CreatePlayer");
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
            if (ModelState.IsValid)
            {
                _repository.Update(player);
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult CreatePlayer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateRecaptcha]
        public IActionResult CreatePlayer(RegisterViewModel player)
        {
            if (_loginManager.IsLoggedIn(HttpContext?.Session))
            {
                return RedirectToAction("Index", "Game");
            }
            if (GetPlayerByUserName(player.Username) != null)
            {
                ViewData.ModelState.TryAddModelError("Username", "There is already a user with this name in the system.");
            }
            if (ModelState.IsValid)
            {
                _repository.Add(new Player()
                {
                    PassWord = _hashing.GetHashedPassword(player.Password),
                    UserName = player.Username,
                    Level = 1
                });
                _emailSender.SendEmailAsync(player.Email,
                    "Welcome to the Shadow World",
                    String.Format(
                        "Welcome to the shadow world! \n\n" +
                        " Your username is: {0} \n" +
                        " Your password is: {1}"
                        , player.Username, player.Password));
                HttpContext?.Session.Clear();
                HttpContext?.Session.SetInt32(ContextData.PlayerId.ToString(),
                    _repository.Read().First(p => p.UserName == player.Username).Id);
                HttpContext?.Session.CommitAsync();
                return RedirectToAction("Index", "Game");
            }
            return View();
        }

        public Player GetPlayerByUserName(string userName)
        {
            return _repository.Read().FirstOrDefault(p => p.UserName == userName);
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateRecaptcha]
        public IActionResult Login(LoginViewModel player)
        {
            if (GetPlayerByUserName(player.Username) == null)
            {
                ModelState.TryAddModelError("Username", "This user does not exist");
            }
            if (!_loginManager.Login(HttpContext.Session, player))
            {
                ModelState.TryAddModelError("Password", "The password was incorrect");
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Game");
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (_loginManager.IsLoggedIn(HttpContext?.Session))
            {
                _loginManager.Logout(HttpContext?.Session);
            }
            return RedirectToAction("Login");
        }
    }
}
