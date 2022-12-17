namespace VladimirTripAdvisor.Logic
{
    public enum ReviewScore
    {
        [Display(Name = "Отлично")]
        Excellent = 5,
        [Display(Name = "Хорошо")]
        Good = 4,
        [Display(Name = "Средне")]
        Average = 3,
        [Display(Name = "Плохо")]
        Poor = 2,
        [Display(Name = "Ужасно")]
        Terrible =1
    }
}
