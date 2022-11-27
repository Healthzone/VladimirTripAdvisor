namespace VladimirTripAdvisor.Models
{
    public class Event
    {
        [Key]
        public long Id { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("telegram_link")]
        [StringLength(maximumLength:500)]
        public string TelegramLink { get; set; }

        [Column("id_creator")]
        [ForeignKey("User")]
        public long CreatorId { get; set; }
        public User User { get; set; }

        [Column("id_place")]
        [ForeignKey("ObjectOfVisit")]
        public long PlaceId { get; set; }
        public ObjectOfVisit ObjectOfVisit { get; set; }
    }
}
