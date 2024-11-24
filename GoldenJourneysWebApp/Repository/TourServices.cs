using GoldenJourneysWebApp.Data;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GoldenJourneysWebApp.Repository
{
    public class TourServices : ITourServices
    {
        public readonly ApplicationDBContext _context;
        public readonly IWebHostEnvironment _environment;
        public TourServices(ApplicationDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public List<TourViewModel> GetTours()
        {
            return _context.Tours.Select(x => new TourViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Location = x.Location,
                Price = x.Price,
                Created = x.Created,
                Status = x.Status,
            }).ToList();
        }
        public List<TourViewModel> FilterTours(string Status)
        {
            return _context.Tours.Where(t => t.Status == Status)
                .Select(x => new TourViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Location = x.Location,
                Price = x.Price,
                Created = x.Created,
                Status = x.Status,
            }).ToList();
        }

        public bool ValidateTourName(TourCreateViewModel model)
        {
            return _context.Tours.Any(t => t.Name == model.Name);
        }


        public void UploadTourDetails(TourCreateViewModel tour)
        {
            var newTour = new Tours
            {
                Name = tour.Name,
                Type = tour.Type,
                Location = tour.Location,
                Price = tour.Price,
                Description = tour.Description,
                Status = "Active",
                Created = DateOnly.FromDateTime(DateTime.Now),
            };
            _context.Tours.Add(newTour);
            _context.SaveChanges();
        }

        public void UploadTourImages(TourCreateViewModel tour)
        {
            var newTour = _context.Tours.FirstOrDefault(t => t.Name == tour.Name);
            int counter = 1;
            foreach (var image in tour.Images)
            {
                string url = InsertImages(image);
                var newTourMedia = new ToursMediaContent
                {
                    TourId = newTour.Id,
                    Title = newTour.Name + counter++,
                    MediaPathURL = url,
                };
                _context.ToursMediaContents.Add(newTourMedia);
                _context.SaveChanges();
            }
        }
        public string InsertImages(IFormFile image)
        {
            string uploadDirectory = Path.Combine(_environment.WebRootPath, "TourImages"); //define the location image gonna be saved
            string fileName = Guid.NewGuid().ToString() + "-" + image.FileName; //set image name and make it unique by adding the GUID
            string filePath = Path.Combine(uploadDirectory, fileName); //defining full file directory

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
               image.CopyTo(fileStream);
            }
            return fileName;
        }


        public void UploadAvailabilitySlots(TourCreateViewModel tour)
        {
            var avaliableDates = CreateDateList(tour.StartDate, tour.EndDate);
            var newTour = _context.Tours.FirstOrDefault(t => t.Name == tour.Name);
            foreach (var date in avaliableDates)
            {
                var newSlot = new TourAvailability
                {
                    TourId = newTour.Id,
                    OriginalCapacity = tour.Capacity,
                    AvailableCapacity = tour.Capacity,
                    AvailableDate = date,
                };
                _context.TourAvailability.Add(newSlot);
                _context.SaveChanges();
            }
        }
        public List<DateOnly> CreateDateList (DateOnly StartDate, DateOnly EndDate)
        {
            List<DateOnly> dateList = new List<DateOnly>();
            for(DateOnly date = StartDate; date < EndDate; date = date.AddDays(1))
            {
                dateList.Add(date);
            }
            return dateList;
        }
    }
}
