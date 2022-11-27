namespace VladimirTripAdvisor.Models
{
    public class Application
    {
        [Key]
        public long Id { get; set; }

        [Column("application_type")]
        public int ApplicationType { get; set; }

        [Column("application_data")]
        public string ApplicationData { get; set; }

        [Column("id_user")]
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
