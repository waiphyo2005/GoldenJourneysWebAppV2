using GoldenJourneysWebApp.Data.Entities;

namespace GoldenJourneysWebApp.Models
{
    public class UserRoomViewandBookingModel
    {
        public int roomId { get; set; }
        public string roomName { get; set; }
        public int roomCapacity { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public List<RoomMediaContent>? gallery { get; set; }
        public int userId { get; set; }
        public DateOnly selectedBookingStartDate { get; set; }
        public DateOnly selectedBookingEndDate { get; set; }
        public int bookingQty { get; set; }
        public string? additionalRequest { get; set; }
    }
}
