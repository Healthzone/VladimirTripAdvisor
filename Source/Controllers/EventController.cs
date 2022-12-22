using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VladimirTripAdvisor.Logic.Telegram;
using VladimirTripAdvisor.Models;
using VladimirTripAdvisor.ViewModels;

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

            return View("MyEvents");
        }

        public IActionResult AllEvents()
        {
            IList<EventModelWithPhotoViewModel> eventList = new List<EventModelWithPhotoViewModel>();

            var events = _db.Event.Where(x => x.EventType == EventType.Public)
                .Where(x => x.StartDate.CompareTo(DateTime.Now.AddHours(-1)) > 0).ToList();

            foreach (var item in events)
            {
                var eventViewModel = new EventModelWithPhotoViewModel();
                eventViewModel.EventModel = item;

                ImageModel? ImageBase64 = _db.Image.Where(x => x.ObjectId == item.PlaceId).FirstOrDefault();
                if(ImageBase64 != null)
                {
                    eventViewModel.ImageMainBase64 = Convert.ToBase64String(ImageBase64.ImageByte);
                }

                ObjectOfVisitModel? place = _db.ObjectOfVisit.Find(item.PlaceId);

                if(place != null)
                {
                    eventViewModel.PlaceName = place.Name;
                }
                eventList.Add(eventViewModel);

            }
            return View(eventList);


        }

        public IActionResult MyEvents()
        {
            IList<EventViewModel> eventList = new List<EventViewModel>();

            var events = _db.Event.Where(x => x.CreatorId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();

            foreach (var item in events)
            {
                var eventViewModel = new EventViewModel();

                eventViewModel.Event = item;
                var place = _db.ObjectOfVisit.Find(item.PlaceId);

                if (place != null)
                {
                    eventViewModel.PlaceName = place.Name;
                }
                eventList.Add(eventViewModel);

            }
            return View(eventList);


        }
    }
}
