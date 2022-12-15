namespace VladimirTripAdvisor.ViewModels
{
    public class ObjectWithPhotoViewModel
    {
        public ObjectWithPhotoViewModel()
        {
            Place = new ObjectOfVisitModel();
        }
        public ObjectOfVisitModel Place{ get; set; }
        public string? ImageBase64{ get; set; }


    }
}
