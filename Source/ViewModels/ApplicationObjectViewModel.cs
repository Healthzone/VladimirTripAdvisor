using System.Buffers.Text;
using VladimirTripAdvisor.Logic.Application;

namespace VladimirTripAdvisor.ViewModels
{
    public class ApplicationObjectViewModel
    {
        public string UserID { get; set; }

        public long? ApplicationId { get; set; }

        public ApplicationStatus? ApplicationStatus { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string? ShortDescription { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string? Latitude { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string? Longitude { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public IFormFile[]? Images { get; set; }

        public string? AdditionalInfoAddress { get; set; }

        [Url]
        public string? ObjectURL { get; set; }

        public string? AdditionalInfoApplication { get; set; }

        public string? AdminComment { get; set; }

    }
}
