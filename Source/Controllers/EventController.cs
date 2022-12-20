using Microsoft.AspNetCore.Mvc;
using VladimirTripAdvisor.Logic.Telegram;
using VladimirTripAdvisor.Models;

namespace VladimirTripAdvisor.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EventController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult CreateEvent(EventModel eventModel)
        {
            Telegram telegram = new Telegram();
            ObjectOfVisitModel? place = _db.ObjectOfVisit.Find(eventModel.PlaceId);
            if (place == null)
            {
                return NotFound();
            }
            string telegramLink = telegram.CreateTelegramChannel(eventModel.Name, eventModel.StartDate, place.Name, place.Id).Result;

            eventModel.TelegramLink = telegramLink;

            _db.Event.Add(eventModel);
            _db.SaveChanges();

            return View();
        }
    }
}
