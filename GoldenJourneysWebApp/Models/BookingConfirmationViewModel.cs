namespace GoldenJourneysWebApp.Models
{
	public class BookingConfirmationViewModel
	{
		public string userName { get; set; }
		public double totalPrice { get; set; }
		public int tourId { get; set; }
		public string tourName { get; set; }
		public DateOnly selectedDate { get; set; }
		public int selectedCapacity { get; set; }
		public int userId { get; set; }
		public string? specialRequest { get; set; }

	}
}
