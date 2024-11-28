using System.Text.RegularExpressions;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace GoldenJourneysWebApp.Controllers
{
    public class HotelController : Controller
    {
        private IHotelServices _hotelService;
        public HotelController(IHotelServices hotelServices)
        {
            _hotelService = hotelServices;
        }


        //View All Hotels
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            var Hotels = _hotelService.GetHotels();
            return View(Hotels);
        }
        //View Filtered Hotels
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult FilterHotels(string Status)
        {
            var Hotels = _hotelService.FilteredHotels(Status);
            return View(Hotels);
        }


        //Create New Hotel
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateHotel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateHotel(CreateHotelViewModel hotel)
        {
            var isNameUsed = _hotelService.ValidateHotelName(hotel);
            if (isNameUsed)
            {
                ModelState.AddModelError("Name", "Hotel name is already used. Please try another name.");
            }
            if (hotel.ThumbnailImage != null)
            {
                string pattern = @"\.(jpg|jpeg|png|gif|bmp)$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                if (!regex.IsMatch(hotel.ThumbnailImage.FileName))
                {
                    ModelState.AddModelError("ThumbnailImage", $"Invalid File Type! Please upload image files only.");
                }
            }
            if (ModelState.IsValid)
            {
                _hotelService.CreateHotel(hotel);
                TempData["Message"] = "Hotel has been successfully created!";
                return RedirectToAction("Index", "Hotel");
            }
            return View(hotel);
        }


        //View Hotel Details
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ViewHotelDetails(int hotelId)
        {
            var hotel = _hotelService.GetHotelById(hotelId);
            return View(hotel);
        }
    }
}
