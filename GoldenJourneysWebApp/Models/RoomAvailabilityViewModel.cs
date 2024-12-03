using GoldenJourneysWebApp.Data.Entities;

namespace GoldenJourneysWebApp.Models
{
    public class RoomAvailabilityViewModel
    {
        public int roomId { get; set; }
        public List<RoomAvailability> availableSlots { get; set; }

    }
}
