using Microsoft.AspNetCore.Mvc;
using VladimirTripAdvisor.Data;

namespace VladimirTripAdvisor.Components
{
    public class CommentaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public CommentaryViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(long? id)
        {
            return View("Commentary");
        }
    }
}
