using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class Hotels
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string StateorRegion { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailImageURL { get; set; }
        [Required]
        public DateOnly Created {  get; set; }
        [Required]
        public string Status { get; set; }
        public ICollection<Rooms> Rooms { get; set; }
    }
}
