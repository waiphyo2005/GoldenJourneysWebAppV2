using GoldenJourneysWebApp.Data.Entities;

namespace GoldenJourneysWebApp.Models
{
    public class RoomDetailsViewModel
    {
        public int roomId { get; set; }
        public int hotelId { get; set; }
        public string hotelName { get; set; }
        public string roomName { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateOnly Created { get; set; }
        public List<RoomMediaContent>? Gallery { get; set; }
        public List<RoomAvailability>? Availability { get; set; }
    }
}
