using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
	public class AddImageViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Image is required.")]
		public List<IFormFile> Images { get; set; }
	}
}
