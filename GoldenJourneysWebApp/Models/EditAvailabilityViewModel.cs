namespace GoldenJourneysWebApp.Models
{
	public class EditAvailabilityViewModel
	{
		public int Id { get; set; }
		public string TourId { get; set; }
		public string Action {  get; set; }
		public DateOnly Date { get; set; }
		public int Capacity { get; set; }

	}
}
