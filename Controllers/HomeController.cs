using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tasks.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy ="Manager")]
        public IActionResult Manager()
        {
            return View();
        }

        [Authorize(Policy ="Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy ="User")]
        public IActionResult UserPage()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}