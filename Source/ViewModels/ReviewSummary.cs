namespace VladimirTripAdvisor.ViewModels
{
    public class ReviewSummary
    {
        public ReviewSummary()
        {
            ExcellentReviews = 0;
            GoodReviews = 0;
            AverageReviews = 0;
            PoorReviews = 0;
            TerribleReviews = 0;
            ReviewsCount= 0;
            AverageRating = 0f;
        }
        public float AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public int ExcellentReviews{ get; set; }
        public int GoodReviews { get; set; }
        public int AverageReviews { get; set; }
        public int PoorReviews { get; set; }
        public int TerribleReviews { get; set; }
    }
}
