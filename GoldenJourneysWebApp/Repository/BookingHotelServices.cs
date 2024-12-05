﻿using GoldenJourneysWebApp.Data;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;

namespace GoldenJourneysWebApp.Repository
{
    public class BookingHotelServices : IBookingHotelServices
    {
        public readonly ApplicationDBContext _context;
        public readonly IWebHostEnvironment _environment;
        public BookingHotelServices (ApplicationDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

		public List<UserAllHotelsViewModel> GetAllHotels()
        {
            var allHotels = _context.Hotels.Where(h => h.Status == "Active")
                .Select(x => new UserAllHotelsViewModel
                {
                    hotelId = x.Id,
                    hotelType = x.Type,
                    hotelStars = x.Stars,
                    HotelName = x.Name,
                    thumbnailImage = x.ThumbnailImageURL,
                    RegionorState = x.StateorRegion
                }).ToList();
            return allHotels;
        }
        public UserHotelDetailsViewModel GetHotelDetails(int hotelId)
		{
            var hoteldetails = _context.Hotels.Where(h => h.Id == hotelId)
                .Select (x => new UserHotelDetailsViewModel
                {
                    hotelId = x.Id,
                    HotelType = x.Type,
                    HotelName = x.Name,
                    HotelDescription = x.Description,
                    HotelLocation = x.Location,
                    HotelStars = x.Stars,
                    Thumbnail = x.ThumbnailImageURL,
                    StateorRegion = x.StateorRegion,
                    Rooms = x.Rooms.ToList(),
                }).FirstOrDefault();
            return hoteldetails;
        }

        public UserRoomViewandBookingModel GetRoomById(int roomId)
        {
            var roomDetails = _context.Rooms.Where(x => x.Id == roomId)
               .Select(t => new UserRoomViewandBookingModel
               {
                   roomId = t.Id,
                   roomName = t.Name,
                   roomCapacity = t.Capacity,
                   price = t.Price,
                   description = t.Description,
                   gallery = t.RoomMediaContent.Where(m => m.RoomId == roomId).ToList(),
                   selectedBookingStartDate = DateOnly.FromDateTime(DateTime.Now),
                   selectedBookingEndDate = DateOnly.FromDateTime(DateTime.Now),
                   hotelName = t.Hotels.Name,
               }).FirstOrDefault();
            return roomDetails;
        }
        public List<DateOnly> CreateDateList(DateOnly StartDate, DateOnly EndDate)
        {
            List<DateOnly> dateList = new List<DateOnly>();
            for (DateOnly date = StartDate; date < EndDate; date = date.AddDays(1))
            {
                dateList.Add(date);
            }
            return dateList;
        }
        public bool isDateAvailable(UserRoomViewandBookingModel booking)
        {
            var dates = CreateDateList(booking.selectedBookingStartDate, booking.selectedBookingEndDate);
            foreach (var date in dates)
            {
                var isUsed = _context.RoomsAvailability.Any(r => r.RoomId == booking.roomId && r.AvailableDate == date);
                if (!isUsed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool isQtyAvailable(UserRoomViewandBookingModel booking)
        {
            var dates = CreateDateList(booking.selectedBookingStartDate, booking.selectedBookingEndDate);
            foreach (var date in dates)
            {
                var availableQty = _context.RoomsAvailability.Where(a => a.RoomId == booking.roomId && a.AvailableDate == date)
                .Select(x => x.AvailableQty).FirstOrDefault();
                if (availableQty < booking.bookingQty)
                {
                    return false;
                }   
            }
            return true;
        }
        public string GetUserNameById(UserRoomViewandBookingModel booking)
        {
            return _context.Users.Where(u => u.Id == booking.userId).Select(u => u.UserName).FirstOrDefault();
        }
        public double CalculateTotalPrice(UserRoomViewandBookingModel booking)
        {
            var totalDates = CreateDateList(booking.selectedBookingStartDate, booking.selectedBookingEndDate).Count();
			double totalPrice = totalDates*(booking.price * booking.bookingQty);
            return totalPrice;
        }
        public void BookRoomQty(HBookingConfirmationViewModel booking)
        {
            var dates = CreateDateList(booking.selectedStartDate, booking.selectedEndDate);
            foreach (var date in dates)
            {
                var availableSlot = _context.RoomsAvailability.Where(a => a.RoomId == booking.roomId && a.AvailableDate == date).FirstOrDefault();
                availableSlot.AvailableQty -= booking.selectedQty;
                _context.RoomsAvailability.Update(availableSlot);
                _context.SaveChanges();
            }
            
        }
        public void RecordBooking(HBookingConfirmationViewModel booking)
        {
            var newBooking = new BookingHotel
            {
                UserId = booking.userId,
                CreatedTime = DateTime.Now,
                TotalPrice = booking.totalPrice,
                Status = "Confirmed",
                SpecialRequest = booking.specialRequest,
                RoomQty = booking.selectedQty,
                FromDate = booking.selectedStartDate,
                ToDate = booking.selectedEndDate,
            };
            _context.BookingHotels.Add(newBooking);
            _context.SaveChanges();

            var bookingId = newBooking.Id;
			var dates = CreateDateList(booking.selectedStartDate, booking.selectedEndDate);
            foreach(var date in dates)
            {
                var newSlot = new RoomBook
                {
                    BookingHotelId = bookingId,
                    RoomAvailabilityId = _context.RoomsAvailability.Where(a => a.RoomId == booking.roomId && a.AvailableDate == date).Select(a => a.Id).FirstOrDefault(),
                };
                _context.BookingBook.Add(newSlot);
                _context.SaveChanges();
            }
		}
		public List<UserHotelBookingsViewModel> GetAllBookings(int userId)
        {
            var bookings = _context.BookingHotels.Where(b => b.UserId == userId)
                .Select(b => new UserHotelBookingsViewModel
                {
                    BookingId = b.Id,
                    HotelName = b.RoomBook.Where(r => r.BookingHotelId == b.Id).FirstOrDefault().RoomAvailability.Rooms.Hotels.Name,
                    RoomName = b.RoomBook.Where(r => r.BookingHotelId == b.Id).FirstOrDefault().RoomAvailability.Rooms.Name,
                    FromDate = b.FromDate,
                    ToDate = b.ToDate,
                    RoomQty = b.RoomQty,
                    SpecialRequest = b.SpecialRequest,
                    Status = b.Status,
                    TotalPrice = b.TotalPrice,
				}).ToList();
            return bookings;
        }
        public HotelCancelBookingViewModel GetBooking(int bookingId)
        {
            var booking = _context.BookingHotels.Where(b => b.Id == bookingId)
                .Select(b => new HotelCancelBookingViewModel
                {
                    bookingId = b.Id,
                    hotelName = b.RoomBook.Where(r => r.BookingHotelId == b.Id).FirstOrDefault().RoomAvailability.Rooms.Hotels.Name,
                    roomName = b.RoomBook.Where(r => r.BookingHotelId == b.Id).FirstOrDefault().RoomAvailability.Rooms.Name,
                    FromDate= b.FromDate,
                    ToDate= b.ToDate,
                    RoomQty= b.RoomQty,
                    TotalPrice= b.TotalPrice,

                }).FirstOrDefault();
            return booking;
        }
        public void CancelBooking(int bookingId)
        {
            var booking = _context.BookingHotels.Where(b => b.Id == bookingId).FirstOrDefault();
            booking.Status = "Cancelled";
            _context.BookingHotels.Update(booking);
            _context.SaveChanges();

            var bookingSlots = _context.BookingBook.Where(b => b.BookingHotelId == bookingId).ToList();
            foreach (var bookingSlot in bookingSlots)
            {
                var availability = _context.RoomsAvailability.Where(a => a.Id == bookingSlot.RoomAvailabilityId).FirstOrDefault();
                availability.AvailableQty += booking.RoomQty;
                _context.RoomsAvailability.Update(availability);
                _context.SaveChanges();
            }
        }


    }
}
