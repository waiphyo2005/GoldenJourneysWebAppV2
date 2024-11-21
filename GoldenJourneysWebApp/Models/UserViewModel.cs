using GoldenJourneysWebApp.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Type { get; set; }
        [Required(ErrorMessage = "Account Status is required.")]
        public string Status { get; set; }
        public DateOnly CreatedAt { get; set; }

    }
}
