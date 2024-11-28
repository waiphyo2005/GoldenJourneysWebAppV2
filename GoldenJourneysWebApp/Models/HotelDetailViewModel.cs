namespace GoldenJourneysWebApp.Models
{
    public class HotelDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Stars { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public DateOnly Created {  get; set; }
        public string Status { get; set; }

    }
}
