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
        public List<DateOnly> CreateDateList(DateOnly StartDate, DateOnly EndDate)
        {
            List<DateOnly> dateList = new List<DateOnly>();
            for (DateOnly date = StartDate; date < EndDate; date = date.AddDays(1))
            {
                dateList.Add(date);
            }
            return dateList;
        }


        public TourViewModel GetTourAllDetails(int Id)
        {
            var tour = _context.Tours.Where(t => t.Id == Id)
                .Select(x => new TourViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Location = x.Location,
                    Price = x.Price,
                    Description = x.Description,
                    Status = x.Status,
                    Created = x.Created,
                    ImageURLs = x.ToursMediaContent.ToList(),
                    AvalibleSlots = x.TourAvailability.ToList(),
                }).SingleOrDefault();
            return tour;
        }
        public TourDetailEditViewModel GetTourDetails(int Id)
        {
            var tour = _context.Tours.Where(t => t.Id == Id)
                .Select(x => new TourDetailEditViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Location = x.Location,
                    Price = x.Price,
                    Description = x.Description,
                    Status = x.Status,
                }).SingleOrDefault();
            return tour;
        }
        public void UploadTourDetails(TourDetailEditViewModel tour)
        {
            Tours existingDetails = _context.Tours.FirstOrDefault(t => t.Id == tour.Id);
            {
                existingDetails.Name = tour.Name;
                existingDetails.Type = tour.Type;
                existingDetails.Location = tour.Location;
                existingDetails.Price = tour.Price;
                existingDetails.Description = tour.Description;
                existingDetails.Status = tour.Status;
                _context.Tours.Update(existingDetails);
                _context.SaveChanges();
            }
        }
        public List<TourGalleryModel> GetTourGallery(int Id)
        {
            var gallery = _context.ToursMediaContents.Where(t => t.TourId == Id)
                .Select(x => new TourGalleryModel
                {
                    Id = x.Id,
                    TourId = x.TourId,
                    Title = x.Title,
                    MediaUrl = x.MediaPathURL,
                }).ToList();
            if (gallery.Count > 0)
            {
                return gallery;
            }
            return null;
        }

        public TourGalleryModel GetImage(int Id)
        {
            var image = _context.ToursMediaContents.Where(t => t.Id == Id)
               .Select(x => new TourGalleryModel
               {
                   Id = x.Id,
                   TourId = x.TourId,
                   Title = x.Title,
                   MediaUrl = x.MediaPathURL,
               }).FirstOrDefault();

            return image;
        }
        public void RemoveImage(int id)
        {
            var RemoveImage = _context.ToursMediaContents.Where(r => r.Id == id).SingleOrDefault();
            _context.ToursMediaContents.Remove(RemoveImage);
            _context.SaveChanges();
        }

        public void AddTourImages(AddImageViewModel image)
        {
            foreach (var pic in image.Images)
            {
                string url = InsertImages(pic);
                var newTourMedia = new ToursMediaContent
                {
                    TourId = image.Id,
                    Title = url,
                    MediaPathURL = url,
                };
                _context.ToursMediaContents.Add(newTourMedia);
                _context.SaveChanges();
            }
        }
        public List<TourAvailability> GetAvailabilitySlots(int tourId)
        {
            var slots = _context.TourAvailability.Where(a => a.TourId == tourId).ToList();
            if (slots.Count > 0)
            {
                return slots;
            }
            return null;
        }
        public bool CheckDuplicateDate(AddAvailabilityViewModel slot)
        {
            var avaliableDates = CreateDateList(slot.StartDate, slot.EndDate);
            foreach (var date in avaliableDates)
            {
                var isUsedDate = _context.TourAvailability.Any(a => a.TourId == slot.tourId && a.AvailableDate == date);
                if (isUsedDate)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddAvailableSlot(AddAvailabilityViewModel slot)
        {
            var avaliableDates = CreateDateList(slot.StartDate, slot.EndDate);
            foreach (var date in avaliableDates)
            {
                var newSlot = new TourAvailability
                {
                    TourId = slot.tourId,
                    OriginalCapacity = slot.Capacity,
                    AvailableCapacity = slot.Capacity,
                    AvailableDate = date,
                };
                _context.TourAvailability.Add(newSlot);
                _context.SaveChanges();
            }
        }
    }
}
