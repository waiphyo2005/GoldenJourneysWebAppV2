using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
    public class TourCreateViewModel
    {
        [Display(Name = "Tour Package Name")]
        [Required(ErrorMessage = "Package Name is required.")]
        public string Name { get; set; }
        [Display(Name = "Package Type")]
        [Required(ErrorMessage = "Tour Type is required.")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Tour Location is required.")]
        public string Location { get; set; }
        [Display(Name = "Price per Pax (USD)")]
        [Required(ErrorMessage = "Price per Customer is required.")]
        public double Price { get; set; }
        public string? Description { get; set; }
		public List<IFormFile>? Images { get; set; }
        [Required(ErrorMessage = "Date Range is required.")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "Date Range is required.")]
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "Capacity is required.")]
        public int Capacity { get; set; }
    }
}
