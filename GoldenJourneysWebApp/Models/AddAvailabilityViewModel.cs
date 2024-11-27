using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
    public class AddAvailabilityViewModel
    {
        public int tourId { get; set; }
        [Required(ErrorMessage = "Date Range is required.")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "Date Range is required.")]
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "Capacity is required.")]
        public int Capacity { get; set; }
    }
}
