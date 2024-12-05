namespace GoldenJourneysWebApp.Models
{
    public class HotelCancelBookingViewModel
    {
        public int bookingId { get; set; }
        public string hotelName { get; set; }
        public string roomName { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public int RoomQty { get; set; }
        public double TotalPrice { get; set; }
    }
}
