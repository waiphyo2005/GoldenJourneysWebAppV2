using System;
using GoldenJourneysWebApp.Data;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Enums;
using GoldenJourneysWebApp.Models;
using static System.Net.Mime.MediaTypeNames;

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
                    States = h.StateorRegion,
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
					States = h.StateorRegion,
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
                Type = hotel.Type.ToString(),
                Stars = hotel.Stars,
                Location = hotel.Location,
                StateorRegion = hotel.States.ToString(),
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
        public bool ValidateHotelName(string name)
        {
            return _context.Hotels.Any(h => h.Name == name);
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
                    State = h.StateorRegion,
                    Description = h.Description,
                    Status = h.Status,
                    Created = h.Created,
                    ThumbnailURL = h.ThumbnailImageURL,
                    HotelRooms = h.Rooms.Select(r => new RoomsViewModel
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Capacity = r.Capacity,
                        Price = r.Price,
                        Created = r.Created,
                    }).ToList(),
                }).FirstOrDefault();
            return hotel;
        }


        //Edit Hotel Details
        public HotelDetailEditViewModel GetHotelDetailById(int id)
        {
            Type enumHotelType = typeof(HotelType);
            Type enumState = typeof(States);
            var hotel = _context.Hotels.Where(h => h.Id == id)
                .Select(h => new HotelDetailEditViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Type = (HotelType)Enum.Parse(enumHotelType, h.Type),
                    Stars = h.Stars,
                    Location = h.Location,
                    States = (States)Enum.Parse(enumState, h.StateorRegion),
                    Description = h.Description,
                    Status = h.Status,
                    ThumbnailURL = h.ThumbnailImageURL,
                }).SingleOrDefault();
            return hotel;
        }
        public void UpdateHotelDetail(HotelDetailEditViewModel hotel)
        {
            if (hotel.newThumbnail != null)
            {
                hotel.ThumbnailURL = HotelInsertImages(hotel.newThumbnail);
            }
            var existingHotel = _context.Hotels.FirstOrDefault(h => h.Id == hotel.Id);
            {
                existingHotel.Name = hotel.Name;
                existingHotel.Type = hotel.Type.ToString();
                existingHotel.Location = hotel.Location;
                existingHotel.StateorRegion = hotel.States.ToString();
                existingHotel.Stars = hotel.Stars;
                existingHotel.Status = hotel.Status;
                existingHotel.Description = hotel.Description;
                existingHotel.ThumbnailImageURL = hotel.ThumbnailURL;
                _context.Hotels.Update(existingHotel);
                _context.SaveChanges();
            }
        }
        public bool ValidateUpdateName(string name, int id)
        {
            return _context.Hotels.Any(h => h.Name == name && h.Id != id);
        }
        public HotelRoomsViewModel GetHotelRoomsById(int id)
        {
            var rooms = _context.Hotels.Where(h => h.Id == id)
                .Select(h => new HotelRoomsViewModel
            {
                HotelId = id,
                HotelName = h.Name,
                HotelRooms = h.Rooms.Select(r => new RoomsViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Price = r.Price,
                    Created = r.Created,
                }).ToList(),
            }).FirstOrDefault();
            return rooms;
        }

        //public bool CheckDuplicateDate(CreateRoomViewModel room)
        //{
        //    var avaliableDates = CreateDateList(room.StartDate, room.EndDate);
        //    foreach (var date in avaliableDates)
        //    {
        //        var isUsedDate = _context.RoomsAvailability.Any(a => a.RoomId == room.id && a.AvailableDate == date);
        //        if (isUsedDate)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public void AddHotelRoom(CreateRoomViewModel hotelRooms)
        {
            var newRoom = new Rooms
            {
                Name = hotelRooms.Name,
                Capacity = hotelRooms.Capacity,
                Price = hotelRooms.Price,
                Description = hotelRooms.Description,
                HotelId = hotelRooms.hotelId,
                Created = DateOnly.FromDateTime(DateTime.Now),
            };
            _context.Rooms.Add(newRoom);
            _context.SaveChanges();
        }
        
        //CHANGE THE NAME TO ID
        public void UploadRoomImages(CreateRoomViewModel room)
        {
            var newRoom = _context.Rooms.FirstOrDefault(r => r.Name == room.Name && r.HotelId == room.hotelId);
            int counter = 1;
            foreach (var image in room.Images)
            {
                string url = InsertImages(image);
                var newRoomMedia = new RoomMediaContent
                {
                    RoomId = newRoom.Id,
                    Title = newRoom.Name + counter++,
                    MediaPathURL = url,
                };
                _context.RoomsMediaContents.Add(newRoomMedia);
                _context.SaveChanges();
            }
        }
        public string InsertImages(IFormFile image)
        {
            string uploadDirectory = Path.Combine(_environment.WebRootPath, "RoomImages"); //define the location image gonna be saved
            string fileName = Guid.NewGuid().ToString() + "-" + image.FileName; //set image name and make it unique by adding the GUID
            string filePath = Path.Combine(uploadDirectory, fileName); //defining full file directory

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return fileName;
        }
        //CHANGE THE NAME TO ID
        public void UploadAvailabilitySlots(CreateRoomViewModel room)
        {
            var avaliableDates = CreateDateList(room.StartDate, room.EndDate);
            var newRoom = _context.Rooms.FirstOrDefault(r => r.Name == room.Name && r.HotelId == room.hotelId);
            foreach (var date in avaliableDates)
            {
                var newRoomType = new RoomAvailability
                {
                    RoomId = newRoom.Id,
                    OriginalQty = room.RoomQty,
                    AvailableQty = room.RoomQty,
                    AvailableDate = date,
                };
                _context.RoomsAvailability.Add(newRoomType);
                _context.SaveChanges();
            }
        }
        public List<DateOnly> CreateDateList(DateOnly StartDate, DateOnly EndDate)
        {
            List<DateOnly> dateList = new List<DateOnly>();
            for (DateOnly date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                dateList.Add(date);
            }
            return dateList;
        }
        public bool RoomNameValidation(CreateRoomViewModel room)
        {
            return _context.Rooms.Any(r => r.HotelId == room.hotelId && r.Name == room.Name);
        }

        public RoomDetailsViewModel GetRoomById(int roomId)
        {
            var room = _context.Rooms.Where(r => r.Id == roomId)
                .Select(r => new RoomDetailsViewModel{
                    roomId = r.Id,
                    hotelId = r.HotelId,
                    hotelName = r.Hotels.Name,
                    roomName = r.Name,
                    Capacity = r.Capacity,
                    Description = r.Description,
                    Price = r.Price,
                    Created = r.Created,
                    Gallery = r.RoomMediaContent.ToList(),
                    Availability = r.RoomAvailability.OrderBy(a => a.AvailableDate).ToList(),
                }).FirstOrDefault();
            return room;
        }
        public RoomDetailsEditViewModel GetRoomDetailsById(int roomId)
        {
            var room = _context.Rooms.Where(r => r.Id == roomId)
                .Select(r => new RoomDetailsEditViewModel
                {
                    roomId = r.Id,
                    roomName = r.Name,
                    Capacity = r.Capacity,
                    Description = r.Description,
                    Price = r.Price,
                    hotelId = r.HotelId,
                    hotelName = r.Hotels.Name,
                }).FirstOrDefault();
            return room;
        }

        public bool RenameRoomNameValidation(RoomDetailsEditViewModel room)
        {
            return _context.Rooms.Any(r => r.HotelId == room.hotelId && r.Name == room.roomName && r.Id != room.roomId);
        }

        public void UpdateRoomDetails(RoomDetailsEditViewModel room)
        {
            var existingRoom = _context.Rooms.Where(r => r.Id == room.roomId).FirstOrDefault();
            {
                existingRoom.Name = room.roomName;
                existingRoom.Capacity = room.Capacity;
                existingRoom.Price = room.Price;
                existingRoom.Description = room.Description;
                _context.Rooms.Update(existingRoom);
                _context.SaveChanges();
            }
        }
        public RoomGalleryViewModel GetRoomGalleryById(int roomId)
        {
            var gallery = _context.RoomsMediaContents.Where(m => m.RoomId == roomId)
                .Select(g => new RoomImageViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    MediaUrl = g.MediaPathURL,
                    roomId = g.RoomId,
                }).ToList();
            var roomGallery = new RoomGalleryViewModel
            {
                roomId = roomId,
                Images = gallery,
            };
            return roomGallery;
        }
        public RoomImageViewModel GetRoomImageById(int imageId)
        {
            var image = _context.RoomsMediaContents.Where(m => m.Id == imageId)
                .Select(i => new RoomImageViewModel
                {
                    Id= i.Id,
                    Title = i.Title,
                    MediaUrl = i.MediaPathURL,
                    roomId= i.RoomId,
                }).FirstOrDefault();
            return image;
        }
        public void RemoveImage(int imageId)
        {
            var RemoveImage = _context.RoomsMediaContents.Where(r => r.Id == imageId).SingleOrDefault();
            _context.RoomsMediaContents.Remove(RemoveImage);
            _context.SaveChanges();
        }
        public void AddRoomImage(AddImageViewModel roomImage)
        {
            foreach (var pic in roomImage.Images)
            {
                string url = InsertImages(pic);
                var newRoomMedia = new RoomMediaContent
                {
                    RoomId = roomImage.Id,
                    Title = url,
                    MediaPathURL = url,
                };
                _context.RoomsMediaContents.Add(newRoomMedia);
                _context.SaveChanges();
            }
        }
    }
}
