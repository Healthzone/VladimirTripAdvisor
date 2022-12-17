using System.ComponentModel.DataAnnotations.Schema;

namespace VladimirTripAdvisor.Models
{
    public class ReviewModel
    {
        public ReviewModel()
        {
            ReviewDate = DateOnly.FromDateTime(DateTime.Now);
        }

        [Key]
        public long Id { get; set; }

        [Column("review_name")]
        [StringLength(maximumLength: 50)]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string ReviewName { get; set; }

        [Column("review_description")]
        [StringLength(maximumLength:1000)]
        [Required(ErrorMessage ="Это поле обязательно для заполнения")] 
        public string ReviewDescription { get; set; }

        [Column("score")]
        public ReviewScore Score { get; set; }

        [Column("review_date")]
        public DateOnly ReviewDate { get; set; }

        [Column("id_object")]
        public long ObjectId { get; set; }

        [ForeignKey("ObjectId")]
        public virtual ObjectOfVisitModel? ObjectOfVisit { get; set; }

        [Column("id_user")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserModel? User { get; set; }
    }
}
