using Microsoft.AspNetCore.Mvc;

namespace VladimirTripAdvisor.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateApplication()
        {
            return View();
        }
    }
}
