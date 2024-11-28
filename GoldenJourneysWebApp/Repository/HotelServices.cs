using GoldenJourneysWebApp.Data;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;

namespace GoldenJourneysWebApp.Repository
{
    public class HotelServices : IHotelServices
    {
        public readonly ApplicationDBContext _context;
        public readonly IWebHostEnvironment _environment;
        public HotelServices(ApplicationDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        //View All Hotels
        public List<HotelViewModel> GetHotels()
        {
            var Hotels = _context.Hotels
                .Select(h => new HotelViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Type = h.Type,
                    Stars = h.Stars,
                    Location = h.Location,
                    Created = h.Created,
                    Status = h.Status,
                }).ToList();
            return Hotels;
        }
        public List<HotelViewModel> FilteredHotels(string Status)
        {
			var Hotels = _context.Hotels.Where(h => h.Status == Status)
				.Select(h => new HotelViewModel
				{
					Id = h.Id,
					Name = h.Name,
					Type = h.Type,
					Stars = h.Stars,
					Location = h.Location,
					Created = h.Created,
					Status = h.Status,
				}).ToList();
			return Hotels;
		}

		//Create New Hotels
		public void CreateHotel(CreateHotelViewModel hotel)
        {
            var newHotel = new Hotels
            {
                Name = hotel.Name,
                Type = hotel.Type,
                Stars = hotel.Stars,
                Location = hotel.Location,
                Description = hotel.Description,
                ThumbnailImageURL = HotelInsertImages(hotel.ThumbnailImage),
                Created = DateOnly.FromDateTime(DateTime.Now),
                Status = "Active",
            };
            _context.Hotels.Add(newHotel);
            _context.SaveChanges();
        }
        public string HotelInsertImages(IFormFile image)
        {
            string uploadDirectory = Path.Combine(_environment.WebRootPath, "HotelImages"); //define the location image gonna be saved
            string fileName = Guid.NewGuid().ToString() + "-" + image.FileName; //set image name and make it unique by adding the GUID
            string filePath = Path.Combine(uploadDirectory, fileName); //defining full file directory

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return fileName;
        }
        public bool ValidateHotelName(CreateHotelViewModel hotel)
        {
            return _context.Hotels.Any(h => h.Name == hotel.Name);
        }


		//View a Hotel Details
		public HotelDetailViewModel GetHotelById(int id)
        {
            var hotel = _context.Hotels.Where(h => h.Id == id)
                .Select(h => new HotelDetailViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Type = h.Type,
                    Stars = h.Stars,
                    Location = h.Location,
                    Description = h.Description,
                    Status = h.Status,
                    Created = h.Created,
                    ThumbnailURL = h.ThumbnailImageURL,
                }).FirstOrDefault();
            return hotel;
        }
	}
}
