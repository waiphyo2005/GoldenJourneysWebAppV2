using System.ComponentModel.DataAnnotations;
using GoldenJourneysWebApp.Enums;

namespace GoldenJourneysWebApp.Models
{
	public class CreateHotelViewModel
	{
		[Required(ErrorMessage = "Hotel Name is required.")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Hotel Type is required.")]
		public HotelType Type { get; set; }
		[Required(ErrorMessage = "Hotel Location is required.")]
		public string Location { get; set; }
        [Required(ErrorMessage = "Hotel State or Region is required.")]
        [Display(Name = "State or Region")]
        public States States { get; set; }
		[Required(ErrorMessage = "Hotel Rating is required.")]
		[Display(Name = "Rating")]
		public int Stars { get; set; }
		public string? Description { get; set; }
        [Display(Name = "Thumbnail Image")]
        [Required(ErrorMessage = "Thumbnail Image is required.")]
        public IFormFile ThumbnailImage { get; set; }
	}
}
