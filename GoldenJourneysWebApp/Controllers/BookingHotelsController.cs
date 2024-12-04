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
                TempData["Message"] = "Tour has been successfully booked!";
                return RedirectToAction("UserTourBookings", "BookingToursController");
            }
            return View(booking);
        }
    }
}
