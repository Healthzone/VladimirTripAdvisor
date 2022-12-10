using VladimirTripAdvisor.Logic.Application;

namespace VladimirTripAdvisor.Models
{
    public class ApplicationModelDB
    {
        [Key]
        public long Id { get; set; }

        [Column("application_type")]
        public ApplicationType ApplicationType { get; set; }
        [Column("application_status")]
        public ApplicationStatus ApplicationStatus { get; set; }

        [Column("application_data")]
        public string ApplicationData { get; set; }

        [Column("id_user")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
    }
}
