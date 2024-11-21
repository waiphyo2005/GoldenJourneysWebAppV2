using System.ComponentModel.DataAnnotations;

namespace GoldenJourneysWebApp.Data.Entities
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
