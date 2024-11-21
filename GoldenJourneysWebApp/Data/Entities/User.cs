using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [Required]
        public int UserTypeId { get; set; }
        [ForeignKey("UserTypeId")]
        public UserType UserType { get; set; }
        [Required]
        public DateOnly CreatedAt { get; set; }
        [Required]
        public string Status { get; set; }
        public ICollection<BookingHotel> BookingHotel { get; set; }
        public ICollection<BookingTour> BookingTour { get; set; }

    }
}
