using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
    public class EditRoomAvailabilityViewModel
    {
        public int slotId { get; set; }
        public int roomId { get; set; }
        public DateOnly Date { get; set; }
        public int OriginalQty { get; set; }
        public int AvailableQty { get; set; }
        [Required(ErrorMessage = "Action is required!")]
        public string Action { get; set; }
        [Required(ErrorMessage = "Quantity is required!")]
        public int ActionQty { get; set; }
    }
}
