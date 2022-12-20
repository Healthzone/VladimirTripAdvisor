namespace VladimirTripAdvisor.ViewModels
{
    public class ObjectWithPhotoViewModel
    {
        public ObjectWithPhotoViewModel()
        {
            Place = new ObjectOfVisitModel();
            Images = new List<string>();
        }
        public ObjectOfVisitModel Place{ get; set; }
        public string? ImageMainBase64{ get; set; }
        public IList<string>? Images { get; set; }

        public ReviewModel ReviewModel { get; set; }

        public EventModel EventModel { get; set; }


    }
}
