using System.Text.RegularExpressions;
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
        public IActionResult Index()
        {
            List<TourViewModel> tours = _tourService.GetTours();
            return View(tours);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult FilterTours(string Status)
        {
            List<TourViewModel> tours = _tourService.FilterTours(Status);
            return View(tours);
        }


		//Create Tours
		[Authorize(Roles = "Admin")]
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

    }
}
