using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using VladimirTripAdvisor.Models;
using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Controllers
{
    public class ObjectController : Controller
    {

        private readonly ApplicationDbContext _db;
        public ObjectController(ApplicationDbContext db)
        {
            _db = db;
        }


        // POST: ObjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult AllObjects()
        {

            IList<ObjectOfVisitModel> objectList = _db.ObjectOfVisit.ToList();
            IList<ObjectWithPhotoViewModel> objectVM = new List<ObjectWithPhotoViewModel>();

            foreach (var item in objectList)
            {
                var obj = new ObjectWithPhotoViewModel()
                {
                    Place = item,
                    ImageBase64 = Convert.ToBase64String(_db.Image.FirstOrDefault(x => x.ObjectId == item.Id).ImageByte)
                };
                objectVM.Add(obj);
            }
            return View(objectVM);
        }
    }
}
