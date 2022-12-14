using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System;
using System.Collections;
using System.Data;
using System.Net.Mime;
using System.Security.Claims;
using System.Xml.Linq;
using VladimirTripAdvisor.Logic.Application;
using VladimirTripAdvisor.Logic.ImageHandler;
using VladimirTripAdvisor.Logic.YandexMap;
using VladimirTripAdvisor.Models;
using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ApplicationController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateApplicationObject()
        {
            if (!User.IsInRole(WC.OwnerRole))
            {
                return Forbid();
            }
            ApplicationObjectViewModel application = new ApplicationObjectViewModel();
            application.UserID = _db.User.Where(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault().Id;
            return View(application);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateApplicationObject(ApplicationObjectViewModel application)
        {
            if (!User.IsInRole(WC.OwnerRole))
            {
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            UserModel? user = _db.User.Where(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            ApplicationJSONSerialization jSONSerialization = new ApplicationJSONSerialization();
            string applicationDataJSON = jSONSerialization.SerializeApplication<ApplicationObjectViewModel>(application);
            if (application.ApplicationStatus == 0 || application.ApplicationStatus == null)
            {
                ApplicationModelDB applicationModelDB = new ApplicationModelDB();
                applicationModelDB.ApplicationData = applicationDataJSON;
                applicationModelDB.ApplicationType = ApplicationType.AddObject;
                applicationModelDB.ApplicationStatus = ApplicationStatus.Created;
                applicationModelDB.UserId = user.Id;
                _db.Application.Add(applicationModelDB);
                _db.SaveChanges();

                ImageHandler imageHandler = new ImageHandler();
                if (application.Images == null)
                {
                    return BadRequest();
                }
                foreach (var file in application.Images)
                {
                    var bytes = imageHandler.ConvertFileToByteArray(file);
                    ImageModel imageModel = new ImageModel()
                    {
                        ImageByte = bytes.Result,
                        ApplicationId = applicationModelDB.Id
                    };
                    _db.Image.Add(imageModel);
                }
                _db.SaveChanges();
            }
            else if (application.ApplicationStatus == ApplicationStatus.Edit)
            {
                ApplicationModelDB? applicationModelDB = _db.Application.Find(application.ApplicationId);
                if (applicationModelDB == null)
                {
                    return NotFound();
                }
                applicationModelDB.ApplicationData = applicationDataJSON;
                applicationModelDB.ApplicationStatus = ApplicationStatus.Processing;
                _db.Application.Update(applicationModelDB);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitApplicationObjectEdit(ApplicationObjectViewModelWithoutRequiredImage application)
        {
            if (!User.IsInRole(WC.OwnerRole))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return View("ViewApplicationObject");
            }

            UserModel? user = _db.User.Where(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            ApplicationJSONSerialization jSONSerialization = new ApplicationJSONSerialization();
            string applicationDataJSON = jSONSerialization.SerializeApplication(application);

            ApplicationModelDB? applicationModelDB = _db.Application.Find(application.ApplicationId);
            if (applicationModelDB == null)
            {
                return NotFound();
            }
            applicationModelDB.ApplicationData = applicationDataJSON;
            applicationModelDB.ApplicationStatus = ApplicationStatus.Processing;
            _db.Application.Update(applicationModelDB);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
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
            ApplicationAccountViewModel applicationData = jSONSerialization.DeserializeApplication<ApplicationAccountViewModel>
                (application.ApplicationData, ApplicationType.UpgradeAccount);


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


            if (application.ApplicationStatus == 0 || application.ApplicationStatus == null)
            {
                ApplicationModelDB applicationModelDB = new ApplicationModelDB();
                applicationModelDB.ApplicationData = jsonApplicationData;
                applicationModelDB.ApplicationType = ApplicationType.UpgradeAccount;
                applicationModelDB.ApplicationStatus = ApplicationStatus.Created;
                applicationModelDB.UserId = application.UserID;

                _db.Application.Add(applicationModelDB);
                _db.SaveChanges();
            }

            if (application.ApplicationStatus == ApplicationStatus.Edit)
            {
                ApplicationModelDB? applicationModelDB = _db.Application.Find(application.ApplicationId);
                if (applicationModelDB == null)
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



        public IActionResult ViewApplication(long? id)
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
            ApplicationJSONSerialization jSONSerialization = new ApplicationJSONSerialization();

            if (application.ApplicationType == ApplicationType.UpgradeAccount)
            {
                var applicationData = jSONSerialization
                   .DeserializeApplication<ApplicationAccountViewModel>(application.ApplicationData, ApplicationType.UpgradeAccount);
                applicationData.ApplicationId = id;
                applicationData.ApplicationStatus = application.ApplicationStatus;

                if (application.ApplicationStatus == ApplicationStatus.Created)
                {
                    application.ApplicationStatus = ApplicationStatus.Taked;
                    _db.Application.Update(application);
                    _db.SaveChanges();
                }
                return View("ViewApplicationAccount", applicationData);
            }
            else
            {
                var applicationData = jSONSerialization
                    .DeserializeApplication<ApplicationObjectViewModelWithoutRequiredImage>(application.ApplicationData, ApplicationType.AddObject);
                applicationData.ApplicationId = id;
                applicationData.ApplicationStatus = application.ApplicationStatus;

                var images = _db.Image.Where(x => x.ApplicationId == id);
                if (images == null)
                {
                    return NotFound();
                }
                applicationData.ImagesBase64 = new List<string>();
                foreach (var image in images)
                {
                    applicationData.ImagesBase64.Add(Convert.ToBase64String(image.ImageByte, 0, image.ImageByte.Length));
                }

                if (application.ApplicationStatus == ApplicationStatus.Created)
                {
                    application.ApplicationStatus = ApplicationStatus.Taked;
                    _db.Application.Update(application);
                    _db.SaveChanges();
                }
                return View("ViewApplicationObject", applicationData);
            }
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

            return RedirectToAction("CheckApplication", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WC.AdminRole)]
        public async Task<IActionResult> SubmitApplicationAccount(long? id)
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

            return RedirectToAction("CheckApplication", "Admin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WC.AdminRole)]
        public IActionResult SubmitApplicationObject(long? id)
        {
            if (!ModelState.IsValid)
            {
                return View("ViewApplicationObject");
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
            var jSONSerialization = new ApplicationJSONSerialization();
            var applicationJsonData = jSONSerialization.DeserializeApplication<ApplicationObjectViewModel>
                (application.ApplicationData, ApplicationType.AddObject);
            application.ApplicationStatus = ApplicationStatus.Done;

            if (applicationJsonData == null)
            {
                return BadRequest();
            }

            ObjectOfVisitModel place = new ObjectOfVisitModel()
            {
                IdOwner = application.UserId,
                Name = applicationJsonData.Name,
                Description = applicationJsonData.Description,
                StreetAddress = applicationJsonData.Address,
                Latitude = applicationJsonData.Latitude,
                Longitude = applicationJsonData.Longitude,
                AdditionalAddressInfo = applicationJsonData.AdditionalInfoAddress,
                PlaceURL = applicationJsonData.ObjectURL,
                PlaceType = applicationJsonData.PlaceType,
                ShortDescription = applicationJsonData.ShortDescription,
                PhoneNumber = applicationJsonData.PhoneNumber,
                AverageRating = 0

            };
            _db.Application.Update(application);
            _db.ObjectOfVisit.Add(place);
            _db.SaveChanges();

            var objectImages = _db.Image.Where(x => x.ApplicationId == application.Id);
            foreach (var item in objectImages)
            {
                item.ObjectId = place.Id;
            }
            _db.UpdateRange(objectImages);
            _db.SaveChanges();

            BaloonJson baloonJson = new BaloonJson(_hostEnvironment);
            baloonJson.AddObject(place, Convert.ToBase64String(objectImages.FirstOrDefault().ImageByte));

            return RedirectToAction("CheckApplication", "Admin");


        }

        [Authorize(Roles = WC.UserRole)]
        public IActionResult MyApplicationAccount()
        {
            IEnumerable<ApplicationModelDB> apps = _db.Application.Where(y => y.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(apps);
        }

        [Authorize(Roles = WC.AdminRole)]
        public IActionResult SendOnEditApplicationAccount(ApplicationAccountViewModel application)
        {
            if (application == null)
            {
                return NotFound();
            }

            ApplicationJSONSerialization jsonSerialization = new ApplicationJSONSerialization();
            string jsonApplicationData = jsonSerialization.SerializeApplication(application);

            ApplicationModelDB? applicationModelDB = _db.Application.Find(application.ApplicationId);

            if (applicationModelDB == null)
            {
                return NotFound();
            }

            applicationModelDB.ApplicationData = jsonApplicationData;
            applicationModelDB.ApplicationStatus = ApplicationStatus.Edit;

            _db.Application.Update(applicationModelDB);
            _db.SaveChanges();

            return RedirectToAction("CheckApplication", "Admin");
        }
        public IActionResult SendOnEditApplicationObject(ApplicationObjectViewModel application)
        {
            if (application == null)
            {
                return NotFound();
            }

            ApplicationJSONSerialization jsonSerialization = new ApplicationJSONSerialization();
            string jsonApplicationData = jsonSerialization.SerializeApplication<ApplicationObjectViewModel>(application);

            ApplicationModelDB? applicationModelDB = _db.Application.Find(application.ApplicationId);

            if (applicationModelDB == null)
            {
                return NotFound();
            }

            applicationModelDB.ApplicationData = jsonApplicationData;
            applicationModelDB.ApplicationStatus = ApplicationStatus.Edit;

            _db.Application.Update(applicationModelDB);
            _db.SaveChanges();

            return RedirectToAction("CheckApplication", "Admin");
        }
    }
}
