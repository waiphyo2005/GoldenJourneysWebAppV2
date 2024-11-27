using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
	public class EditAvailabilityViewModel
	{
		public int Id { get; set; }
		public int TourId { get; set; }
        public DateOnly Date { get; set; }
        public int OriginalCapacity { get; set; }
        public int AvailableCapacity { get; set; }
        [Required(ErrorMessage = "Action is required!")]
        public string Action { get; set; }
        [Required(ErrorMessage = "Capacity is required!")]
        public int ActionCapacity { get; set; }

    }
}
