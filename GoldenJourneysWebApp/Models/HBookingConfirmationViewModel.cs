namespace GoldenJourneysWebApp.Models
{
    public class HBookingConfirmationViewModel
    {
        public string userName { get; set; }
        public double totalPrice { get; set; }
        public int roomId { get; set; }
        public string roomName { get; set; }
        public string hotelName { get; set; }
        public DateOnly selectedStartDate { get; set; }
        public DateOnly selectedEndDate { get; set; }
        public int selectedQty { get; set; }
        public int userId { get; set; }
        public string? specialRequest { get; set; }
    }
}
