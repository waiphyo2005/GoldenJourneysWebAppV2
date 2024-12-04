using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoldenJourneysWebApp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class BookingToursController : Controller
    {
        private IBookingTourServices _bookingservices;
        public BookingToursController(IBookingTourServices bookingTourServices)
        {
            _bookingservices = bookingTourServices;
        }

        [HttpGet]
        public IActionResult SelectedTourBooking(int tourId)
        {
            ViewData["userId"] = Convert.ToInt32(HttpContext.User.Identity.Name);
            var tourDetails = _bookingservices.GetTourDetails(tourId);
            return View(tourDetails);
        }
        [HttpPost]
        public IActionResult SelectedTourBooking(TourDetailandBookingViewModel booking)
        {
			ViewData["userId"] = booking.userId;

			var isDateAvailable = _bookingservices.isDateAvailable(booking);
            if (!isDateAvailable)
            {
                ModelState.AddModelError("selectedBookingDate", "Tour is not available for the selected date. Please try another date.");
            }
            var isCapacityAvailable = _bookingservices.isCapacityAvailable(booking);
            if (!isCapacityAvailable)
            {
                ModelState.AddModelError("bookingCapacity", "Tour is not available for the selected date. Please try another date.");
            }
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        Console.WriteLine($"Property: {state.Key}");
                        foreach (var error in state.Value.Errors)
                        {
                            Console.WriteLine($"Error: {error.ErrorMessage}");
                        }
                    }
                }
                return View(booking); // Return the view to show validation errors
            }
            if (ModelState.IsValid)
            {
                TempData["BookingData"] = JsonConvert.SerializeObject(booking);
                return RedirectToAction("BookingConfirm", "BookingTours");
            }
            return View(booking);
        }

        [HttpGet]
        public IActionResult BookingConfirm()
        {
            var bookingJson = TempData["BookingData"] as string;
            var booking = JsonConvert.DeserializeObject<TourDetailandBookingViewModel>(bookingJson);
            var bookingConfirm = new BookingConfirmationViewModel
            {
                userId = booking.userId,
                tourName = booking.tourName,
                tourId = booking.tourId,
                selectedCapacity = booking.bookingCapacity,
                selectedDate = booking.selectedBookingDate,
                specialRequest = booking.additionalRequest,
                totalPrice = _bookingservices.CalculateTotalPrice(booking),
                userName = _bookingservices.GetUserNameById(booking),
            };
			return View(bookingConfirm);
        }
        [HttpPost]
        public IActionResult BookingConfirm(BookingConfirmationViewModel booking)
        {
            if (ModelState.IsValid)
            {
                _bookingservices.BookTourCapacity(booking);
                _bookingservices.RecordBooking(booking);
				TempData["Message"] = "Tour has been successfully booked!";
				return RedirectToAction("UserTourBookings", "BookingTours");
            }
            return View(booking);
        }

        [HttpGet]
        public IActionResult UserTourBookings()
        {
            var userId = Convert.ToInt32(HttpContext.User.Identity.Name);
            var bookings = _bookingservices.GetBookingsById(userId);
            return View(bookings);
		}
        [HttpGet]
        public IActionResult CancelBooking(int bookingId)
        {
            var booking = _bookingservices.GetBooking(bookingId);
            return View(booking);
        }
        public IActionResult ConfirmCancelBooking(int bookingId)
        {
            _bookingservices.CancelBooking(bookingId);
            TempData["Message"] = "Booking has been Cancelled!";
            return RedirectToAction("UserTourBookings", "BookingTours");
        }
    }
}
