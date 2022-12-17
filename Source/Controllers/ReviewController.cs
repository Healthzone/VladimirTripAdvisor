using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace VladimirTripAdvisor.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ReviewController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(ReviewModel reviewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            _db.Add(reviewModel);
            _db.SaveChanges();

            UpdateAverageObjectRating(reviewModel.ObjectId);


            return RedirectToAction("ViewObject", "Object", new {id = reviewModel.ObjectId});
        }

        private void UpdateAverageObjectRating(long placeId)
        {
            var placeReviews = _db.Review.Where(x => x.ObjectId == placeId);
            float averageRating = 0;
            foreach (var review in placeReviews)
            {
                averageRating += (float) review.Score;
            }
            averageRating /= placeReviews.Count();
            averageRating = MathF.Round(averageRating, 2);

            var place = _db.ObjectOfVisit.Find(placeId);
            place.AverageRating = averageRating;
            _db.Update(place);
            _db.SaveChanges();
        }
    }
}
