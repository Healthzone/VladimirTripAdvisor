using System.ComponentModel.DataAnnotations.Schema;

namespace VladimirTripAdvisor.Models
{
    public class ReviewModel
    {
        public ReviewModel()
        {
            ReviewDate = DateTime.Now;
        }

        [Key]
        public long Id { get; set; }

        [Column("review_name")]
        [StringLength(maximumLength: 50)]
        [Required(ErrorMessage = "Данное поле обязательно")]
        public string ReviewName { get; set; }

        [Column("review_description")]
        [StringLength(maximumLength:1000)]
        [Required(ErrorMessage ="Данное поле обязательно")] 
        public string ReviewDescription { get; set; }

        [Column("score")]
        public ReviewScore Score { get; set; }

        [Column("review_date")]
        [Required(ErrorMessage ="Данное поле обязательно")]
        public DateTime ReviewDate { get; set; }

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
