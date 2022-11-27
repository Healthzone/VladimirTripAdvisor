using System.ComponentModel.DataAnnotations.Schema;

namespace VladimirTripAdvisor.Models
{
    public class Review
    {
        [Key]
        public long Id { get; set; }

        [Column("review_description")]
        [StringLength(maximumLength:500)]
        [Required(ErrorMessage ="Это поле обязательно для заполнения")] 
        public string ReviewDescription { get; set; }

        [Column("score")]
        public int Score { get; set; }

        [Column("id_object")]
        [ForeignKey("ObjectOfVisit")]
        public long ObjectId { get; set; }
        public ObjectOfVisit? ObjectOfVisit { get; set; }

        [ForeignKey("User")]
        [Column("id_user")]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
