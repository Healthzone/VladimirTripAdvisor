using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VladimirTripAdvisor.Logic.Application;
using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Controllers
{
    public class ApplicationController : Controller
    {

        private readonly ApplicationDbContext _db;
        public ApplicationController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateApplication()
        {
            return View();
        }

        [Authorize(Roles = WC.UserRole)]
        public IActionResult UpgradeAccount()
        {
            UserModel? user = _db.User.Where(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
            if (user == null)
            {
                NotFound(user);
            }
            ApplicationDataViewModel applicationData = new ApplicationDataViewModel()
            {
                UserID = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Midname = user.Midname,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
                
            };
            return View(applicationData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WC.UserRole)]
        public IActionResult UpgradeAccount(ApplicationDataViewModel application)
        {
            if (!ModelState.IsValid)
            {
                return View(application);
            }
            ApplicationJSONSerialization jsonSerialization = new ApplicationJSONSerialization();
            string jsonApplicationData = jsonSerialization.SerializeApplication(application);

            ApplicationModelDB applicationModelDB = new ApplicationModelDB();
            applicationModelDB.ApplicationData = jsonApplicationData;
            applicationModelDB.ApplicationType = ApplicationType.UpgradeAccount;
            applicationModelDB.ApplicationStatus = ApplicationStatus.Created;
            applicationModelDB.UserId = application.UserID;

            _db.Application.Add(applicationModelDB);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = WC.AdminRole)]
        public IActionResult CheckApplication()
        {
            IEnumerable<ApplicationModelDB> apps = _db.Application.Where(x => x.ApplicationType == ApplicationType.UpgradeAccount).Include(x => x.User);
            return View(apps);
        }
    }
}
