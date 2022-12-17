using Microsoft.AspNetCore.Mvc;
using VladimirTripAdvisor.Data;
using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Components
{
    public class CommentarySummary : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public CommentarySummary(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? id)
        {
            var reviews = _db.Review.Where(x => x.ObjectId == id);
            ReviewSummary reviewSummary = new ReviewSummary();

            reviewSummary.ReviewsCount = reviews.Count();

            var place = await _db.ObjectOfVisit.FindAsync(id);
            reviewSummary.AverageRating = (float)place.AverageRating;

            foreach (var review in reviews)
            {
                switch (review.Score)
                {
                    case ReviewScore.Excellent:
                        reviewSummary.ExcellentReviews++;
                        break;
                    case ReviewScore.Good:
                        reviewSummary.GoodReviews++;
                        break;
                    case ReviewScore.Average:
                        reviewSummary.AverageReviews++;
                        break;
                    case ReviewScore.Poor:
                        reviewSummary.PoorReviews++;
                        break;
                    case ReviewScore.Terrible:
                        reviewSummary.TerribleReviews++;
                        break;
                    default:
                        break;
                }
            }
            return View("CommentarySummary", reviewSummary);
        }
    }
}
