﻿using System.Text.RegularExpressions;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            if (tour.Images != null)
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

                if (tour.Images != null)
                {
                    _tourService.UploadTourImages(tour);
                }
                _tourService.UploadAvailabilitySlots(tour);

                TempData["Message"] = "Tour Package has been successfully created!";
                return RedirectToAction("Index", "Tour");
            }

            return View(tour);
        }


        //View Tour Details
        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            TourViewModel tourDetails = _tourService.GetTourAllDetails(id);
            return View(tourDetails);
        }


        //Edit Tour Details (Name, location, etc..)
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
        [HttpGet]
        public IActionResult EditGallery(int id)
        {
            var tourGallery = _tourService.GetTourGallery(id);
            if(tourGallery == null)
            {
                return RedirectToAction("AddImages", "Tour", new { id = id });
            }
            return View(tourGallery);
        }
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
        [HttpGet]
        public ActionResult AddImages(int id)
        {
            ViewData["TourId"] = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddImages(AddImageModel image)
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
            if (ModelState.IsValid)
            {
                _tourService.AddTourImages(image);
                TempData["Message"] = "Image has been successfully added!";
                return RedirectToAction("EditGallery", "Tour", new { id = image.Id });
            }
            return View(image);
            
        }
    }
}
