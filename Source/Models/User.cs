using System.ComponentModel.DataAnnotations;

namespace VladimirTripAdvisor.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(maximumLength: 45, MinimumLength = 4)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(maximumLength: 256, MinimumLength = 8)]
        public string Password { get; set; }

        public int AccountType { get; set; }

        [StringLength(maximumLength:50)]
        public string Name { get; set; }

        [StringLength(maximumLength: 50)]
        public string Surname { get; set; }
    }
}
