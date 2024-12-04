using GoldenJourneysWebApp.Data.Entities;

namespace GoldenJourneysWebApp.Models
{
    public class TourDetailandBookingViewModel
    {
        public int tourId { get; set; }
        public string tourName { get; set; }
        public string tourType { get; set; }
        public string location { get; set; }
        public double price { get; set; }
        public string? description { get; set; }
        public string StateorRegion { get; set; }
        public List<ToursMediaContent>? gallery { get; set; }
        public int userId { get; set; }
        public DateOnly selectedBookingDate { get; set; }
        public int bookingCapacity { get; set; }
        public string? additionalRequest {  get; set; }
    }
}
