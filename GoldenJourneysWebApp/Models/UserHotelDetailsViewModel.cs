using GoldenJourneysWebApp.Data.Entities;

namespace GoldenJourneysWebApp.Models
{
    public class UserHotelDetailsViewModel
    {
        public int hotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelType { get; set; }
        public int HotelStars { get; set; }
        public string HotelLocation { get; set; }
        public string HotelDescription { get; set; }
        public string Thumbnail { get; set; }
        public string StateorRegion { get; set; }
        public List<Rooms> Rooms { get; set; }

    }
}
