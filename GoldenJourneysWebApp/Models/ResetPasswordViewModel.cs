using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
	public class ResetPasswordViewModel
	{
        public int userId { get; set; }
		public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
