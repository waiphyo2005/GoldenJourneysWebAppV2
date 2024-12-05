using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoldenJourneysWebApp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class BookingHotelsController : Controller
    {
        private IBookingHotelServices _bookingHotelServices;
        public BookingHotelsController(IBookingHotelServices bookingHotelServices)
        {
            _bookingHotelServices = bookingHotelServices;
        }

        [HttpGet]
        public IActionResult GetHotelDetails(int hotelId)
        {
            var hotel = _bookingHotelServices.GetHotelDetails(hotelId);
            return View(hotel);
        }

        [HttpGet]
        public IActionResult GetRoomDetails(int roomId)
        {
            ViewData["userId"] = Convert.ToInt32(HttpContext.User.Identity.Name);
            var hotel = _bookingHotelServices.GetRoomById(roomId);
            return View(hotel);
        }

        [HttpPost]
        public IActionResult GetRoomDetails(UserRoomViewandBookingModel booking)
        {
            ViewData["userId"] = booking.userId;
            if(booking.selectedBookingStartDate >= booking.selectedBookingEndDate)
            {
                ModelState.AddModelError("selectedBookingStartDate", "Check-in date must be prior to Check-out date.");
            }
            var isDateAvailable = _bookingHotelServices.isDateAvailable(booking);
            if (!isDateAvailable)
            {
                ModelState.AddModelError("selectedBookingStartDate", "Room is not available for the selected dates. Please try another date.");
                ModelState.AddModelError("selectedBookingEndDate", "Room is not available for the selected dates. Please try another date.");
            }
            var isCapacityAvailable = _bookingHotelServices.isQtyAvailable(booking);
            if (!isCapacityAvailable)
            {
                ModelState.AddModelError("bookingQty", "Room is not available for the selected dates. Please try another date.");
            }

            if (ModelState.IsValid)
            {
                TempData["BookingData"] = JsonConvert.SerializeObject(booking);
                return RedirectToAction("BookingConfirm", "BookingHotels");
            }
            return View(booking);
        }

        [HttpGet]
        public IActionResult BookingConfirm()
        {
            var bookingJson = TempData["BookingData"] as string;
            var booking = JsonConvert.DeserializeObject<UserRoomViewandBookingModel>(bookingJson);
            var bookingConfirm = new HBookingConfirmationViewModel
            {
                userId = booking.userId,
                roomName = booking.roomName,
                roomId = booking.roomId,
                selectedQty = booking.bookingQty,
                selectedStartDate = booking.selectedBookingStartDate,
                selectedEndDate = booking.selectedBookingEndDate,
                specialRequest = booking.additionalRequest,
                totalPrice = _bookingHotelServices.CalculateTotalPrice(booking),
                userName = _bookingHotelServices.GetUserNameById(booking),
                hotelName = booking.hotelName,
            };
            return View(bookingConfirm);
        }
        [HttpPost]
        public IActionResult BookingConfirm(HBookingConfirmationViewModel booking)
        {
            if (ModelState.IsValid)
            {
                _bookingHotelServices.BookRoomQty(booking);
                _bookingHotelServices.RecordBooking(booking);
                TempData["Message"] = "Hotel has been successfully booked!";
                return RedirectToAction("UserHotelBookings", "BookingHotels");
            }
            return View(booking);
        }

        [HttpGet]
        public IActionResult UserHotelBookings()
        {
			var userId = Convert.ToInt32(HttpContext.User.Identity.Name);
            var bookings = _bookingHotelServices.GetAllBookings(userId);
			return View(bookings);
        }

        [HttpGet]
        public IActionResult CancelBooking(int bookingId)
        {
            var booking = _bookingHotelServices.GetBooking(bookingId);
            return View(booking);
        }
        public IActionResult ConfirmCancel(int bookingId)
        {
            _bookingHotelServices.CancelBooking(bookingId);
            TempData["Message"] = "Booking has been Cancelled!";
            return RedirectToAction("UserHotelBookings", "BookingHotels");
        }
    }
}
