using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Data;
using System.Security.Claims;
using VladimirTripAdvisor.Logic.Application;
using VladimirTripAdvisor.Models;
using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateApplicationObject()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateApplicationObject(ApplicationObjectViewModel application)
        {
            return View();
        }

        [Authorize(Roles = WC.UserRole)]
        public IActionResult CreateApplicationAccount()
        {
            if (User.IsInRole(WC.OwnerRole))
            {
                return Forbid();
            }
            UserModel? user = _db.User.Where(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
            if (user == null)
            {
                return NotFound(user);
            }
            ApplicationAccountViewModel applicationData = new ApplicationAccountViewModel()
            {
                UserID = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Midname = user.Midname,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email

            };
            return View("UpgradeAccountApplication", applicationData);
        }

        [Authorize(Roles = WC.UserRole)]
        public IActionResult UpgradeAccountApplication(long? id)
        {
            if (User.IsInRole(WC.OwnerRole))
            {
                return Forbid();
            }
            ApplicationModelDB? application = _db.Application.Find(id);

            if (application == null)
            {
                return NotFound();
            }
            ApplicationJSONSerialization jSONSerialization = new ApplicationJSONSerialization();
            ApplicationAccountViewModel applicationData = jSONSerialization.DeserializeApplication(application.ApplicationData);


            return View(applicationData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WC.UserRole)]
        public IActionResult UpgradeAccountApplication(ApplicationAccountViewModel application)
        {
            if (User.IsInRole(WC.OwnerRole))
            {
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                return View(application);
            }
            ApplicationJSONSerialization jsonSerialization = new ApplicationJSONSerialization();
            string jsonApplicationData = jsonSerialization.SerializeApplication(application);


            if(application.ApplicationStatus == 0 || application.ApplicationStatus == null)
            {
                ApplicationModelDB applicationModelDB = new ApplicationModelDB();
                applicationModelDB.ApplicationData = jsonApplicationData;
                applicationModelDB.ApplicationType = ApplicationType.UpgradeAccount;
                applicationModelDB.ApplicationStatus = ApplicationStatus.Created;
                applicationModelDB.UserId = application.UserID;

                _db.Application.Add(applicationModelDB);
                _db.SaveChanges();
            }

            if(application.ApplicationStatus == ApplicationStatus.Edit)
            {
                ApplicationModelDB? applicationModelDB = _db.Application.Find(application.ApplicationId);
                if(applicationModelDB == null)
                {
                    return NotFound();
                }
                applicationModelDB.ApplicationData = jsonApplicationData;
                applicationModelDB.ApplicationStatus = ApplicationStatus.Processing;
                _db.Application.Update(applicationModelDB);
                _db.SaveChanges();
            }



            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = WC.AdminRole)]
        public IActionResult CheckApplication()
        {
            IEnumerable<ApplicationModelDB> apps = _db.Application.Where(x => x.ApplicationType == ApplicationType.UpgradeAccount).Include(x => x.User);
            return View(apps);
        }

        public IActionResult ViewApp(long? id)
        {
            if(User.IsInRole(WC.OwnerRole))
            {
                return Forbid();
            }
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ApplicationModelDB? application = _db.Application.Find(id);

            if (application == null)
            {
                return NotFound();
            }
            ApplicationJSONSerialization jSONSerialization = new ApplicationJSONSerialization();
            ApplicationAccountViewModel applicationData = jSONSerialization.DeserializeApplication(application.ApplicationData);
            applicationData.ApplicationId = id;
            applicationData.ApplicationStatus = application.ApplicationStatus;

            if(application.ApplicationStatus != ApplicationStatus.Taked && application.ApplicationStatus != ApplicationStatus.Edit)
            {
                application.ApplicationStatus = ApplicationStatus.Taked;
                _db.Application.Update(application);
                _db.SaveChanges();
            }

            return View(applicationData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WC.AdminRole)]
        public IActionResult DenyApp(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ApplicationModelDB? application = _db.Application.Find(id);

            if (application == null)
            {
                return NotFound();
            }

            application.ApplicationStatus = ApplicationStatus.Canceled;

            _db.Application.Update(application);
            _db.SaveChanges();

            return RedirectToAction("CheckApplication");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WC.AdminRole)]
        public async Task<IActionResult> SubmitApp(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ApplicationModelDB? application = _db.Application.Find(id);

            if (application == null)
            {
                return NotFound();
            }
            IdentityUser user = await _userManager.FindByIdAsync(application.UserId);

            if (user == null)
            {
                return NotFound();
            }

            await _userManager.AddToRoleAsync(user, WC.OwnerRole);

            application.ApplicationStatus = ApplicationStatus.Done;
            _db.Application.Update(application);
            await _db.SaveChangesAsync();

            return RedirectToAction("CheckApplication");
        }

        [Authorize(Roles = WC.UserRole)]
        public IActionResult MyApplicationAccount()
        {
            IEnumerable<ApplicationModelDB> apps = _db.Application.Where(x => x.ApplicationType == ApplicationType.UpgradeAccount)
                .Where(y => y.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(apps);
        }

        [Authorize(Roles = WC.AdminRole)]
        public IActionResult SendOnEditApp(ApplicationAccountViewModel application)
        {
            if (application == null)
            {
                return NotFound();
            }

            ApplicationJSONSerialization jsonSerialization = new ApplicationJSONSerialization();
            string jsonApplicationData = jsonSerialization.SerializeApplication(application);

            ApplicationModelDB? applicationModelDB = _db.Application.Find(application.ApplicationId);

            if(applicationModelDB == null)
            {
                return NotFound();
            }

            applicationModelDB.ApplicationData = jsonApplicationData;
            applicationModelDB.ApplicationStatus = ApplicationStatus.Edit;

            _db.Application.Update(applicationModelDB);
            _db.SaveChanges();

            return RedirectToAction("CheckApplication");
        }
    }
}
