﻿
namespace VladimirTripAdvisor.ViewModels
{
    public class ApplicationDataViewModel
    {
        public string UserID { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [MaxLength(50, ErrorMessage = "Данное поле не должно превышать 50 символов")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [MaxLength(50, ErrorMessage = "Данное поле не должно превышать 50 символов")]
        public string? Surname { get; set; }

        [MaxLength(50, ErrorMessage = "Данное поле не должно превышать 50 символов")]
        public string? Midname { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [MaxLength(12, ErrorMessage = "Данное поле не должно превышать 50 символов")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string? AdditionalInfo { get; set; }

    }
}
