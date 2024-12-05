namespace GoldenJourneysWebApp.Models
{
    public class UserHotelBookingsViewModel
    {
        public int BookingId { get; set; }
        public string HotelName { get; set; }
        public string RoomName { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public int RoomQty { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public string SpecialRequest { get; set; }
    }
}
