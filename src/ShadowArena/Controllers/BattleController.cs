using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shadow_Arena.BattleSystem;
using Shadow_Arena.Enumerations;
using Shadow_Arena.Repositories;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Shadow_Arena.Controllers
{
    public class BattleController : Controller
    {
        private ILoginManager _loginManager;
        private readonly IPlayerRepository _repo;
        private readonly ICharacterRepository _charRepo;

        public BattleController(ILoginManager loginManager, IPlayerRepository repo, ICharacterRepository charRepo)
        {
            _loginManager = loginManager;
            this._repo = repo;
            _charRepo = charRepo;
        }

        // GET: /BattleController/
        public IActionResult Index()
        {
            if (!_loginManager.IsLoggedIn(HttpContext?.Session))
                return RedirectToAction("Login", "Player");

            if (BattleOrchestrator.GetBattle(HttpContext.Session.GetInt32(ContextData.PlayerId.ToString()).GetValueOrDefault()) != null)
                return RedirectToAction("Battle");
            if (BattleOrchestrator.IsNotFighting(HttpContext.Session.GetInt32(ContextData.PlayerId.ToString()).GetValueOrDefault()))
            {
                return View();
            }
            return RedirectToAction("Waiting");
        }

        public IActionResult Waiting()
        {
            if (BattleOrchestrator.GetBattle(HttpContext.Session.GetInt32(ContextData.PlayerId.ToString()).GetValueOrDefault()) != null)
                return RedirectToAction("Battle");
            return View();
        }
        public IActionResult FindBattle()
        {
            if (!_loginManager.IsLoggedIn(HttpContext?.Session))
                return RedirectToAction("Login", "Player");
            var players = _repo.Read();
            foreach (var player in players) //add characters to the players
            {
                player.Character =
                    _charRepo.Read()
                        .Where(
                            c =>
                                c.OwningPlayerid.GetValueOrDefault() != null &&
                                c.OwningPlayerid.GetValueOrDefault() == player.Id).ToList();
            }
            BattleOrchestrator.FindBattle(players,
                HttpContext.Session.GetInt32(ContextData.PlayerId.ToString()).GetValueOrDefault());
            return RedirectToAction("Waiting");
        }

        public IActionResult Fight(int CharacterId)
        {
            BattleOrchestrator.Fight(CharacterId, HttpContext.Session.GetInt32(ContextData.PlayerId.ToString()).GetValueOrDefault());
                return RedirectToAction("Battle");
        }
    }
}
