namespace GoldenJourneysWebApp.Models
{
    public class RoomsViewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }
        public DateOnly Created { get; set; }
    }
}
