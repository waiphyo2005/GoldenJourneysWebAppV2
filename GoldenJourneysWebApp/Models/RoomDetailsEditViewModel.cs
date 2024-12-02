using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
    public class RoomDetailsEditViewModel
    {
        public int roomId { get; set; }
        public int hotelId { get; set; }
        public string hotelName { get; set; }
        [Required(ErrorMessage = "Room Type Name is required.")]
        [Display(Name = "Room Name")]
        public string roomName { get; set; }
        [Required(ErrorMessage = "Room Capacity is required.")]
        public int Capacity { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Room Price is required.")]
        public double Price { get; set; }
    }
}
