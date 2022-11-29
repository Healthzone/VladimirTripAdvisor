using System.ComponentModel.DataAnnotations.Schema;

namespace VladimirTripAdvisor.Models
{
    public class ReviewModel
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
        public long ObjectId { get; set; }
        [ForeignKey("ObjectId")]
        public virtual ObjectOfVisitModel? ObjectOfVisit { get; set; }

        [Column("id_user")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
    }
}
