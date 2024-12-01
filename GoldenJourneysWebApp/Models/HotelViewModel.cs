namespace GoldenJourneysWebApp.Models
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Stars { get; set; }
        public string States { get; set; }
        public DateOnly Created {  get; set; }
        public string Status { get; set; }
    }
}
