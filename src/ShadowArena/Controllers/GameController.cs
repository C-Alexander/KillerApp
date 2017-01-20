using Microsoft.AspNetCore.Mvc;
using Shadow_Arena.Contexts;
using Shadow_Arena.Repositories;

namespace Shadow_Arena.Controllers
{
    public class GameController : Controller
    {
        private LoginManager _loginManager;

        public GameController(IDatabaseManager databaseManager)
        {
            _loginManager = new LoginManager(new PlayerRepository(new PlayerSQLContext(databaseManager)));
        }

        public IActionResult Index()
        {
            if (_loginManager.IsLoggedIn(HttpContext?.Session))
            {
                return View();
            }
            return RedirectToAction("Login", "Player");
        }
    }
}