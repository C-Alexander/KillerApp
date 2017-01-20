using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shadow_Arena.Contexts;
using Shadow_Arena.Enumerations;
using Shadow_Arena.Repositories;

namespace Shadow_Arena.Controllers
{
    public class HomeController : Controller
    {
        private PlayerRepository _repository;

        public HomeController(IDatabaseManager databaseManager)
        {
            _repository = new PlayerRepository(new PlayerSqlContext(databaseManager));
        }

    public IActionResult Index()
        {
            if (HttpContext.Session.GetString(ContextData.PlayerId.ToString()) != null)
            {
                if (_repository.Read(HttpContext.Session.GetInt32(ContextData.PlayerId.ToString()).GetValueOrDefault()) != null)
                {
                    return RedirectToAction("Index", "Game");
                }
            }
            return RedirectToAction("CreatePlayer", "Player");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
