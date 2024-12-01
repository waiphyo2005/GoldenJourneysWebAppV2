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
            var isNameUsed = _hotelService.ValidateHotelName(hotel.Name);
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


        //Edit a hotel's details
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditHotelDetails(int hotelId)
        {
            var hotel = _hotelService.GetHotelDetailById(hotelId);
            return View(hotel);
        }
        [HttpPost]
        public IActionResult EditHotelDetails(HotelDetailEditViewModel hotel)
        {
            var isNameUsed = _hotelService.ValidateUpdateName(hotel.Name, hotel.Id);
            if (isNameUsed)
            {
                ModelState.AddModelError("Name", "Hotel name is already used. Please try another name.");
            }
            if (hotel.newThumbnail != null)
            {
                string pattern = @"\.(jpg|jpeg|png|gif|bmp)$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                if (!regex.IsMatch(hotel.newThumbnail.FileName))
                {
                    ModelState.AddModelError("newThumbnail", $"Invalid File Type! Please upload image files only.");
                }
            }
            if (ModelState.IsValid)
            {
                _hotelService.UpdateHotelDetail(hotel);
                TempData["Message"] = "Hotel details has been successfully updated";
                return RedirectToAction("ViewHotelDetails", "Hotel", new {hotelId = hotel.Id});
            }
            return View(hotel);
        }


        //View Rooms
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ViewRooms(int hotelId)
        {
            var rooms = _hotelService.GetHotelRoomsById(hotelId);
            return View(rooms);
        }

        //Add New Room
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddRooms(int hotelId)
        {
            ViewData["hotelId"] = hotelId;
            return View();
        }
        [HttpPost]
        public IActionResult AddRooms(CreateRoomViewModel room)
        {
            var isRoomNameUsed = _hotelService.RoomNameValidation(room);
            if (isRoomNameUsed)
            {
                ModelState.AddModelError("Name", "Room Type Name already exist for this Hotel. Please try another name.");
            }
            if (room.Price < 0)
            {
                ModelState.AddModelError("Price", "Invalid Room Price.");
            }
            if (room.Capacity < 0)
            {
                ModelState.AddModelError("Capacity", "Invalid Room Capacity.");
            }
            if(room.RoomQty < 1)
            {
                ModelState.AddModelError("RoomQty", "Invalid Room Quantity.");
            }
            if (room.StartDate > room.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start Date must be earlier than the End Date.");
                ModelState.AddModelError("EndDate", "End Date must be later than the Start Date.");
            }
            if (room.StartDate < DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError("StartDate", "Invalid Start Date! The date must not be prior than today.");
            }
            if (room.Images != null)
            {
                string pattern = @"\.(jpg|jpeg|png|gif|bmp)$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                foreach (var image in room.Images)
                {
                    if (!regex.IsMatch(image.FileName))
                    {
                        ModelState.AddModelError("Images", $"Invalid File Type for {image.FileName}! Please upload image files only.");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                _hotelService.AddHotelRoom(room);
                _hotelService.UploadRoomImages(room);
                _hotelService.UploadAvailabilitySlots(room);
                TempData["Message"] = "Room has been successfully created!";
                return RedirectToAction("ViewRooms", "Hotel", new { hotelId = room.hotelId });
            }
            return View(room);
        }


        //View Room Details
        public IActionResult ViewRoomDetails (int roomId)
        {
            return View();
        }
    }
}
