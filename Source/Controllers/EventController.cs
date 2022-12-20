using Microsoft.AspNetCore.Mvc;

namespace VladimirTripAdvisor.Controllers
{
    public class EventController : Controller
    {
        public IActionResult CreateEvent()
        {
            return View();
        }
    }
}
