using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class Tours
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public double Price { get; set; }

        public string? Description { get; set; }
        [Required]
        public DateOnly Created { get; set; }
        [Required]
        public string Status { get; set; }
        public ICollection<ToursMediaContent> ToursMediaContent { get; set; }
        public ICollection<TourAvailability> TourAvailability { get; set; }
    }
}
