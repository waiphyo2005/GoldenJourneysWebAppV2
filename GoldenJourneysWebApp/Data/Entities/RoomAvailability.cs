using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class RoomAvailability
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Rooms Rooms { get; set; }
        public DateOnly AvailableDate { get; set; }
        public int OriginalQty { get; set; }
        public int AvailableQty { get; set; }
        public ICollection<RoomBook> RoomBook { get; set; }
    }
}
