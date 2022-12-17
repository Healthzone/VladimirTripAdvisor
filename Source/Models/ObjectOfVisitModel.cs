using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VladimirTripAdvisor.Models
{
    [Table("object_of_visit")]
    public class ObjectOfVisitModel
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(maximumLength: 1500)]
        [Column("place_description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(maximumLength: 250)]
        [Column("place_short_description")]
        public string ShortDescription { get; set; }

        [Column("place_type")]
        public PlaceType PlaceType { get; set; }

        [StringLength(maximumLength: 50)]
        public string Latitude { get; set; }

        [StringLength(maximumLength: 50)]
        public string Longitude { get; set; }

        [StringLength(maximumLength: 150)]
        [Column("street_address")]
        public string StreetAddress { get; set; }

        [StringLength(maximumLength: 150)]
        [Column("additional_address_info")]
        public string? AdditionalAddressInfo { get; set; }

        [Column("average_rating")]
        public float? AverageRating { get; set; }

        [Phone]
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [StringLength(maximumLength: 150)]
        [Column("place_url")]
        public string? PlaceURL { get; set; }

        [Column("id_owner")]
        public string? IdOwner { get; set; }
        [ForeignKey("IdOwner")]
        public UserModel? User { get; set; }
    }
}
