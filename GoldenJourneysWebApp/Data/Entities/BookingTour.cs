using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class BookingTour
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        public int TourAvailabilityId { get; set; }
        [ForeignKey("TourAvailabilityId")]
        public TourAvailability TourAvailability { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public string? Status { get; set; }
        public string? SpecialRequest { get; set; }
    }
}
