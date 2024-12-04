namespace GoldenJourneysWebApp.Models
{
	public class TourBookingCancelViewModel
	{
		public int BookingId { get; set; }
		public string TourName { get; set; }
		public DateOnly BookedDate { get; set; }
		public int BookedCapacity { get; set; }
		public double TotalPrice { get; set; }
	}
}
