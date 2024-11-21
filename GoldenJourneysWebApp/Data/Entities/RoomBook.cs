using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class RoomBook
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoomAvailabilityId { get; set; }
        [ForeignKey("RoomAvailabilityId")]
        public RoomAvailability RoomAvailability { get; set; }
        [Required]
        public int BookingHotelId { get; set; }
        [ForeignKey("BookingHotelId")]
        public BookingHotel BookingHotel { get; set; }
    }
}
