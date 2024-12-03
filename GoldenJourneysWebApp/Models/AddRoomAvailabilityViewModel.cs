using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
	public class AddRoomAvailabilityViewModel
	{
		public int roomId { get; set; }
		[Required(ErrorMessage = "Date Range is required.")]
		[Display(Name = "From Date")]
		public DateOnly StartDate { get; set; }
		[Required(ErrorMessage = "Date Range is required.")]
		[Display(Name = "To Date")]
		public DateOnly EndDate { get; set; }
		[Required(ErrorMessage = "Room Quantity is required.")]
		public int Quantity { get; set; }
	}
}
