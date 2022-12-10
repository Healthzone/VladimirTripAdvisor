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

        // GET: ObjectController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: ObjectController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ObjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ObjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ObjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
