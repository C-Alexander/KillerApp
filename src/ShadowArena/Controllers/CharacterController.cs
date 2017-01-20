using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shadow_Arena.Enumerations;
using Shadow_Arena.Models;
using Shadow_Arena.Repositories;

namespace Shadow_Arena.Controllers
{
    public class CharacterController : Controller
    {
        private ILoginManager _loginManager;
        private readonly IClassRepository _classRepo;
        private readonly ICharacterRepository _repo;

        public CharacterController(ILoginManager loginManager, IClassRepository classRepo, ICharacterRepository repo)
        {
            _loginManager = loginManager;
            _classRepo = classRepo;
            _repo = repo;
        }

        public IActionResult Index()
        {
            HttpContext.Session.LoadAsync();
            if (!_loginManager.IsLoggedIn(HttpContext?.Session))
            {
                return RedirectToAction("Login", "Player");
            }
            return
                View(
                    _repo.Read()
                        .Where(
                            c =>
                                c.OwningPlayerid != null &&
                                c.OwningPlayerid == HttpContext.Session.GetInt32(ContextData.PlayerId.ToString())));
        }
        [HttpGet]
        public IActionResult CreateCharacter()
        {
            if (_loginManager.IsLoggedIn(HttpContext?.Session))
            {
                ViewBag.Classes = _classRepo.Read().Select(c
              => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
                return View();
            }
                return RedirectToAction("Login", "Player");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCharacter(CreateCharacterViewModel character)
        {
            if (!_loginManager.IsLoggedIn(HttpContext?.Session))
            {
                return RedirectToAction("Login", "Player");
            }
            if (
                _repo.Read()
                    .Count(c => c.OwningPlayerid == HttpContext.Session.GetInt32(ContextData.PlayerId.ToString())) > 3)
                ModelState.TryAddModelError(String.Empty, "You already own too many characters.");
            

            if (ModelState.IsValid)
            {
                _repo.Add(new Character()
                {
                    Name = character.Name,
                    Classid = character.ClassId,
                    OwningPlayerid = HttpContext?.Session.GetInt32(ContextData.PlayerId.ToString())
                });
                return RedirectToAction("Index");
            }
            ViewBag.Classes = _classRepo.Read().Select(c
=> new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            return RedirectToAction("CreateCharacter");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!_loginManager.IsLoggedIn(HttpContext?.Session) || !_repo.Read().Any(c => 
            c.OwningPlayerid == HttpContext.Session.GetInt32(ContextData.PlayerId.ToString()) 
            && c.Id == id)) //ensure they do own the character.
            {
                return RedirectToAction("Login", "Player");
            }
            _repo.Delete(new Character() {Id = id});
            return RedirectToAction("Index");
        }
    }
}