namespace VladimirTripAdvisor.Models
{
    public class EventModel
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
        public long CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual UserModel User { get; set; }

        [Column("id_place")]
        public long PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public virtual ObjectOfVisitModel ObjectOfVisit { get; set; }
    }
}
