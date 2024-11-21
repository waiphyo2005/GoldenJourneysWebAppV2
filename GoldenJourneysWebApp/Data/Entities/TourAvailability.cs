using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class TourAvailability
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TourId { get; set; }
        [ForeignKey("TourId")]
        public Tours Tours { get; set; }
        [Required]
        public DateOnly AvailableDate {  get; set; }
        [Required]
        public int AvailableCapacity { get; set; }
        public ICollection<BookingTour> BookingTour { get; set; }
    }
}
