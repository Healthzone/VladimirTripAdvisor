using Microsoft.AspNetCore.Mvc;
using VladimirTripAdvisor.Data;
using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Components
{
    public class Commentary : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public Commentary(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(long? id)
        {
            var reviews = _db.Review.Where(x => x.ObjectId == id).OrderByDescending(x => x.ReviewDate);
            IList<ReviewViewModel> reviewViewModels = new List<ReviewViewModel>();
            foreach (var review in reviews.ToList())
            {
                var userReview = _db.User.Find(review.UserId);
                review.ReviewDate = review.ReviewDate.Date;
                ReviewViewModel reviewView = new ReviewViewModel()
                {
                    Review = review,
                    Name = userReview.Name,
                    Surname = userReview.Surname,
                };
                reviewViewModels.Add(reviewView);
            }
            return View("Commentary", reviewViewModels);
        }
    }
}
