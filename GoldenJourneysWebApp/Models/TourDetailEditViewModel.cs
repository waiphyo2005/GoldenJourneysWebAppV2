using System.ComponentModel.DataAnnotations;
using GoldenJourneysWebApp.Enums;

namespace GoldenJourneysWebApp.Models
{
    public class TourDetailEditViewModel
    {
		[Display(Name = "Package Id")]
		public int Id { get; set; }
        [Display(Name = "Tour Package Name")]
        [Required(ErrorMessage = "Package Name is required.")]
        public string Name { get; set; }
        [Display(Name = "Package Type")]
        [Required(ErrorMessage = "Tour Type is required.")]
        public TourType Type { get; set; }
        [Required(ErrorMessage = "Tour Location is required.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Hotel State or Region is required.")]
        [Display(Name = "State or Region")]
        public States States { get; set; }
        [Display(Name = "Price per Pax (USD)")]
        [Required(ErrorMessage = "Price per Customer is required.")]
        public double Price { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Package Status is required.")]
        public string Status { get; set; }
    }
}
