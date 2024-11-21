using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class ToursMediaContent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string MediaPathURL { get; set; }
        [Required]
        public int TourId { get; set; }
        [ForeignKey("TourId")]
        public Tours Tours { get; set; }
    }
}
