using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotels Hotels { get; set; }
        public DateOnly Created { get; set; }
        public ICollection<RoomMediaContent> RoomMediaContent { get; set; }
        public ICollection<RoomAvailability> RoomAvailability { get; set; }
    }
}
