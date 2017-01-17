using Microsoft.AspNetCore.Mvc;

namespace Shadow_Arena.Controllers
{
    public class GameController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}