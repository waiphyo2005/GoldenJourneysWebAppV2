using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
    public class TourViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public DateOnly Created {  get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public List<string> ImageURLs { get; set; }

    }
}
