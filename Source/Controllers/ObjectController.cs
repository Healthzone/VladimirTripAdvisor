using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
