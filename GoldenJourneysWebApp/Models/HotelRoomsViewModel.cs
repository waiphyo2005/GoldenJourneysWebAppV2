namespace GoldenJourneysWebApp.Models
{
    public class HotelRoomsViewModel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public List<RoomsViewModel> HotelRooms { get; set; }
    }
}
