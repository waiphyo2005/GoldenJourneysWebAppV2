using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
    public class CreateRoomViewModel
    {
        public int hotelId {  get; set; }
        [Required(ErrorMessage = "Room Type Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Room Capacity is required.")]
        public int Capacity { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price per night is required.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "At least one Image is required.")]
        public List<IFormFile> Images { get; set; }
        [Required(ErrorMessage = "Date Range is required.")]
        [Display(Name = "From Date")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "Date Range is required.")]
        [Display(Name = "To Date")]
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "Room Quantity is required.")]
        public int RoomQty { get; set; }

    }
}
