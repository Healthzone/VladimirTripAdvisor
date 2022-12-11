using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VladimirTripAdvisor.Models
{
    public class UserModel : IdentityUser
    {
        
        [StringLength(maximumLength:50)]
        public string Name { get; set; }

        [StringLength(maximumLength: 50)]
        public string Surname { get; set; }

        [StringLength(maximumLength: 50)]
        public string? Midname { get; set; }
    }
}
