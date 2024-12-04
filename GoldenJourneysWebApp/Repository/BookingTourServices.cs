using GoldenJourneysWebApp.Data;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using Microsoft.Identity.Client;

namespace GoldenJourneysWebApp.Repository
{
    public class BookingTourServices : IBookingTourServices
    {
        public readonly ApplicationDBContext _context;
        public readonly IWebHostEnvironment _environment;
        public BookingTourServices (ApplicationDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public List<UserAllToursViewModel> AllTours()
        {
            var allTours = _context.Tours.Where(x => x.Status == "Active")
                .Select(t => new UserAllToursViewModel
                {
                    tourId = t.Id,
                    tourName = t.Name,
                    type = t.Type,
                    StateorRegion = t.StateorRegion,
                    price = t.Price,
                    thumbnailImage = t.ToursMediaContent.Where(m => m.TourId == t.Id).Select(m => m.MediaPathURL).FirstOrDefault(),
                }).ToList();
            return allTours;
        }

        public TourDetailandBookingViewModel GetTourDetails(int tourId)
        {
            var tourDetails = _context.Tours.Where(x => x.Id == tourId)
                .Select(t => new TourDetailandBookingViewModel
                {
                    tourId = t.Id,
                    tourName = t.Name,
                    tourType = t.Type,
                    location = t.Location,
                    price = t.Price,
                    description = t.Description,
                    StateorRegion = t.StateorRegion,
                    gallery = t.ToursMediaContent.Where(m => m.TourId == tourId).ToList(),
                }).FirstOrDefault();
            return tourDetails;
        }
        public bool isDateAvailable(TourDetailandBookingViewModel booking)
        {
            return _context.TourAvailability.Any(a => a.TourId == booking.tourId && a.AvailableDate == booking.selectedBookingDate);
        }
        public bool isCapacityAvailable(TourDetailandBookingViewModel booking)
        {
            var availableCapacity = _context.TourAvailability.Where(a => a.TourId == booking.tourId && a.AvailableDate == booking.selectedBookingDate)
                .Select(x => x.AvailableCapacity).FirstOrDefault();
            if (availableCapacity > booking.bookingCapacity)
            {
                return true;
            }
            return false;
        }
        public string GetUserNameById(TourDetailandBookingViewModel booking)
        {
            return _context.Users.Where(u => u.Id == booking.userId).Select(u => u.UserName).FirstOrDefault();
        }
        public double CalculateTotalPrice(TourDetailandBookingViewModel booking)
        {
            double totalPrice = booking.price*booking.bookingCapacity;
            return totalPrice;
        }

        public void BookTourCapacity(BookingConfirmationViewModel booking)
        {
            var availableSlot = _context.TourAvailability.Where(a => a.TourId == booking.tourId && a.AvailableDate == booking.selectedDate).FirstOrDefault();
            availableSlot.AvailableCapacity -= booking.selectedCapacity;
            _context.TourAvailability.Update(availableSlot);
            _context.SaveChanges();
        }
        public void RecordBooking(BookingConfirmationViewModel booking)
        {
            var newBooking = new BookingTour
            {
                UserId = booking.userId,
                TourAvailabilityId = _context.TourAvailability.Where(a => a.TourId == booking.tourId && a.AvailableDate == booking.selectedDate).Select(a => a.Id).FirstOrDefault(),
                CreatedTime = DateTime.Now,
                NumberOfPeople = booking.selectedCapacity,
                TotalPrice = booking.totalPrice,
                Status = "Confirmed",
                SpecialRequest = booking.specialRequest,
            };
            _context.BookingTours.Add(newBooking);
            _context.SaveChanges();
        }

		public List<UserTourBookingsViewModel> GetBookingsById(int userId)
        {
            var bookings = _context.BookingTours.Where(b => b.UserId == userId)
                .Select(t => new UserTourBookingsViewModel
                {
                    bookingId = t.Id,
                    userName = t.User.UserName,
                    bookedCapacity = t.NumberOfPeople,
                    bookedDate = t.TourAvailability.AvailableDate,
                    tourName = t.TourAvailability.Tours.Name,
                    totalPrice = t.TotalPrice,
                    status = t.Status,
                    specialRequest = t.SpecialRequest,
                    created = t.CreatedTime,
                }).ToList();
            return bookings;
        }
        public TourBookingCancelViewModel GetBooking(int bookingId)
        {
            var booking = _context.BookingTours.Where(b => b.Id == bookingId)
                .Select(t => new TourBookingCancelViewModel
                {
                    BookingId = t.Id,
                    TourName = t.TourAvailability.Tours.Name,
                    BookedDate = t.TourAvailability.AvailableDate,
                    BookedCapacity = t.NumberOfPeople,
                    TotalPrice = t.TotalPrice,
                }).FirstOrDefault();
            return booking;
        }
        public void CancelBooking(int bookingId)
        {
            var booking = _context.BookingTours.Where(b => b.Id == bookingId).FirstOrDefault();
            booking.Status = "Cancelled";
            _context.BookingTours.Update(booking);
            _context.SaveChanges();

            var availability = _context.TourAvailability.Where(a => a.Id == booking.TourAvailabilityId).FirstOrDefault();
            availability.AvailableCapacity += booking.NumberOfPeople;
            _context.TourAvailability.Update(availability);
            _context.SaveChanges();
        }
    }
}
