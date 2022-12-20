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
        public async Task<IActionResult> AllObjects()
        {

            IList<ObjectOfVisitModel> objectList = _db.ObjectOfVisit.ToList();
            IList<ObjectWithPhotoViewModel> objectVM = new List<ObjectWithPhotoViewModel>();

            foreach (var item in objectList)
            {
                ImageModel imgByte = await _db.Image.FirstOrDefaultAsync(x => x.ObjectId == item.Id);
                var obj = new ObjectWithPhotoViewModel()
                {
                    Place = item,
                    ImageMainBase64 = Convert.ToBase64String(imgByte.ImageByte)
                };
                objectVM.Add(obj);
            }
            return View(objectVM);
        }

        public IActionResult ViewObject(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = _db.ObjectOfVisit.Find(id);
            if (place == null)
            {
                return NotFound();
            }
            var objectVM = new ObjectWithPhotoViewModel()
            {
                Place = place
            };
            var objectImages = _db.Image.Where(x => x.ObjectId == id);
            foreach (var item in objectImages)
            {
                objectVM.Images.Add(Convert.ToBase64String(item.ImageByte));
            }
            return View(objectVM);
        }

        public IActionResult RemoveObject(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ObjectOfVisitModel? place = _db.ObjectOfVisit.Find(id);

            if(place == null)
            {
                return BadRequest();
            }
            _db.ObjectOfVisit.Remove(place);
            _db.SaveChanges();

            return RedirectToAction("AllObjects");

        }
    }
}
