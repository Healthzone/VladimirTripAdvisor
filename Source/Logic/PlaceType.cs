namespace VladimirTripAdvisor.Logic
{
    public enum PlaceType
    {
        [Display(Name = "Заведение")]
        Attraction = 1,
        [Display(Name = "Культурный объект")]
        Cultural = 2,
        [Display(Name = "Ресторан")]
        Restaurant = 3,
        [Display(Name = "Жилье")]
        Accommodation =4
    }
}
