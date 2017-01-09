using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shadow_Arena.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("../Player/Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
