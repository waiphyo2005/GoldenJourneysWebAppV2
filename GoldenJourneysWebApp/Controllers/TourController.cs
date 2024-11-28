using System.Text.RegularExpressions;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace GoldenJourneysWebApp.Controllers
{
    public class TourController : Controller
    {
        private ITourServices _tourService;

        public TourController(ITourServices tourServices)
        {
            _tourService = tourServices;
        }


        //View Tours
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            List<TourViewModel> tours = _tourService.GetTours();
            return View(tours);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult FilterTours(string Status)
        {
            List<TourViewModel> tours = _tourService.FilterTours(Status);
            return View(tours);
        }


		//Create Tours
		[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateTour()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTour(TourCreateViewModel tour)
        {

            var isNameUsed = _tourService.ValidateTourName(tour);
            if (isNameUsed)
            {
                ModelState.AddModelError("Name", "Package name is already used. Please try another name.");
            }

            if (tour.StartDate >= tour.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start Date must be earlier than the End Date.");
                ModelState.AddModelError("EndDate", "End Date must be later than the Start Date.");
            }
            if(tour.Images != null)
            {
                string pattern = @"\.(jpg|jpeg|png|gif|bmp)$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                foreach (var image in tour.Images)
                {
                    if (!regex.IsMatch(image.FileName))
                    {
                        ModelState.AddModelError("Images", $"Invalid File Type for {image.FileName}! Please upload image files only.");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _tourService.UploadTourDetails(tour);
                _tourService.UploadTourImages(tour);
                _tourService.UploadAvailabilitySlots(tour);

                TempData["Message"] = "Tour Package has been successfully created!";
                return RedirectToAction("Index", "Tour");
            }

            return View(tour);
        }


        //View Tour Details
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            TourViewModel tourDetails = _tourService.GetTourAllDetails(id);
            return View(tourDetails);
        }


        //Edit Tour Details (Name, location, etc..)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditTourDetails(int id)
        {
            TourDetailEditViewModel tourDetails = _tourService.GetTourDetails(id);
            return View(tourDetails);
        }

        [HttpPost]
        public IActionResult EditTourDetails(TourDetailEditViewModel tour)
        {
            if (ModelState.IsValid)
            {
                _tourService.UploadTourDetails(tour);
                TempData["Message"] = "Tour Package Details has been successfully updated!";
                return RedirectToAction("ViewDetails", "Tour", new { id = tour.Id });
            }
            return View(tour);
        }


        //Edit Tour Details (Gallery)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditGallery(int id)
        {
            var tourGallery = _tourService.GetTourGallery(id);
            return View(tourGallery);
        }

        //Delete Image
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult SelectDeleteImage(int id)
        {
            var image = _tourService.GetImage(id);
            return View(image);
        }
        public IActionResult RemoveImage(int id, int tourId)
        {
            _tourService.RemoveImage(id);
            TempData["Message"] = "Image has been successfully removed!";
            return RedirectToAction("EditGallery", "Tour", new { id = tourId });
        }

        //Add Image
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddImages(int id)
        {
            ViewData["TourId"] = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddImages(AddImageViewModel image)
        {
            ViewData["TourId"] = image.Id;
            if (image.Images != null)
            {
                string pattern = @"\.(jpg|jpeg|png|gif|bmp)$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                foreach (var pic in image.Images)
                {
                    if (!regex.IsMatch(pic.FileName))
                    {
                        ModelState.AddModelError("Images", $"Invalid File Type for {pic.FileName}! Please upload image files only.");
                    }
                }
            }
            if (image.Images == null)
            {
                ModelState.AddModelError("Images", $"Please Select an Image.");
            }
            if (ModelState.IsValid)
            {
                _tourService.AddTourImages(image);
                TempData["Message"] = "Image has been successfully added!";
                return RedirectToAction("EditGallery", "Tour", new { id = image.Id });
            }
            return View(image);
        }


        //Edit Tour Availability Slots
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ViewAvailabilitySlots(int tourId)
        {
            var tourSlots = _tourService.GetAvailabilitySlots(tourId);
            if (tourSlots != null)
            {
                return View(tourSlots);
            }
            return RedirectToAction("AddAvailabilitySlot", "Tour", new { tourId = tourId });
        }

        //Add Availability Slots
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddAvailabilitySlot(int tourId)
        {
            ViewData["tourId"]=tourId;
            return View();
        }

        [HttpPost]
        public IActionResult AddAvailabilitySlot(AddAvailabilityViewModel slot)
        {
            ViewData["tourId"] = slot.tourId;
            if (slot.StartDate >= slot.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start Date must be earlier than the End Date.");
                ModelState.AddModelError("EndDate", "End Date must be later than the Start Date.");
            }
            if(slot.StartDate <= DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError("StartDate", "Invalid Start Date! The date must not be prior than today.");
            }
            var isDateExist = _tourService.CheckDuplicateDate(slot);
            if (isDateExist)
            {
                ModelState.AddModelError("StartDate", "Selected date or dates already exist. Cannot create new slot.");
                ModelState.AddModelError("StartDate", "Selected date or dates already exist. Cannot create new slot.");
            }
            if (ModelState.IsValid)
            {
                _tourService.AddAvailableSlot(slot);
                TempData["Message"] = "New Slot has been successfully added!";
                return RedirectToAction("ViewAvailabilitySlots", "Tour", new { tourId = slot.tourId });
            }
            return View(slot);
        }

        //Edit Availability Slots
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditAvailabilitySlot(int id)
        {
            var slot = _tourService.GetAvailableSlot(id);
            return View(slot);
        }
        [HttpPost]
        public IActionResult EditAvailabilitySlot(EditAvailabilityViewModel slot)
        {
            if(slot.Action == null)
            {
                ModelState.AddModelError("Action", "Please select the action.");
            }
            if (slot.Action == "Deduct")
            {
                if (slot.ActionCapacity > slot.AvailableCapacity)
                {
                    ModelState.AddModelError("ActionCapacity", "Not Enough Available Capacity to Deduct!");
                }
            }
            if (ModelState.IsValid)
            {
                _tourService.EditCapacity(slot);
                TempData["Message"] = "Slot has been successfully updated!";
                return RedirectToAction("ViewAvailabilitySlots", "Tour", new { tourId = slot.TourId });
            }
            return View(slot);
        }
    }
}
