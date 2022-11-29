namespace VladimirTripAdvisor.Models
{
    [Table("image_object")]
    public class ImageModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Column("image_byte")]
        public byte[] ImageByte { get; set; }

        [Column("id_object")]
        public long ObjectId { get; set; }
        [ForeignKey("ObjectId")]
        public virtual ObjectOfVisitModel? ObjectOfVisit { get; set; }
    }
}
