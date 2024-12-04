namespace GoldenJourneysWebApp.Models
{
    public class UserTourBookingsViewModel
    {
        public int bookingId { get; set; }
        public string tourName { get; set; }
        public string userName { get; set; }
        public DateOnly bookedDate { get; set; }
        public int bookedCapacity { get; set; }
        public string status { get; set; }
        public string specialRequest { get; set; }
        public double totalPrice { get; set; }
        public DateTime created { get; set; }
    }
}
