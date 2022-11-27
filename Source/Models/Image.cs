namespace VladimirTripAdvisor.Models
{
    [Table("image_object")]
    public class Image
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Column("image_byte")]
        public byte[] ImageByte { get; set; }

        [Column("id_object")]
        [ForeignKey("ObjectOfVisit")]
        public long ObjectId { get; set; }
        public ObjectOfVisit? ObjectOfVisit { get; set; }
    }
}
