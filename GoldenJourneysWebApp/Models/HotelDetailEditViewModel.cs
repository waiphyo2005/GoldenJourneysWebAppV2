using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
    public class HotelDetailEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Hotel Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Hotel Type is required.")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Hotel Rating is required.")]
        public int Stars { get; set; }
        [Required(ErrorMessage = "Hotel Location is required.")]
        public string Location { get; set; }
        public string? Description { get; set; }
        public string ThumbnailURL { get; set; }
        [Required(ErrorMessage = "Hotel Status is required.")]
        public string Status { get; set; }
        public IFormFile? newThumbnail {  get; set; }
    }
}
