using GoldenJourneysWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoldenJourneysWebApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tours> Tours { get; set; }
        public DbSet<ToursMediaContent> ToursMediaContents { get; set; }
        public DbSet<TourAvailability> TourAvailability { get; set; }
        public DbSet<BookingTour> BookingTours { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomMediaContent> RoomsMediaContents { get; set; }
        public DbSet<RoomAvailability> RoomsAvailability { get; set; }
        public DbSet<BookingHotel> BookingHotels { get; set; }
        public DbSet<RoomBook> BookingBook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
