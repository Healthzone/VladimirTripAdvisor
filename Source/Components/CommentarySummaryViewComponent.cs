using Microsoft.AspNetCore.Mvc;
using VladimirTripAdvisor.Data;

namespace VladimirTripAdvisor.Components
{
    public class CommentarySummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public CommentarySummaryViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(long? id)
        {
            return View("CommentarySummary");
        }
    }
}
