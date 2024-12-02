using GoldenJourneysWebApp.Models;

namespace GoldenJourneysWebApp.Repository
{
    public interface IHotelServices
    {
        List<HotelViewModel> GetHotels();
        List<HotelViewModel> FilteredHotels(string Status);
        void CreateHotel(CreateHotelViewModel hotel);
        bool ValidateHotelName(string name);
        HotelDetailViewModel GetHotelById (int id);
        HotelDetailEditViewModel GetHotelDetailById(int id);
        void UpdateHotelDetail(HotelDetailEditViewModel hotel);
        bool ValidateUpdateName(string name, int id);
        HotelRoomsViewModel GetHotelRoomsById(int id);
        void AddHotelRoom(CreateRoomViewModel hotelRooms);
        void UploadRoomImages(CreateRoomViewModel room);
        void UploadAvailabilitySlots(CreateRoomViewModel room);
        bool RoomNameValidation(CreateRoomViewModel room);
        RoomDetailsViewModel GetRoomById(int roomId);
        RoomDetailsEditViewModel GetRoomDetailsById(int roomId);
        bool RenameRoomNameValidation(RoomDetailsEditViewModel room);
        void UpdateRoomDetails(RoomDetailsEditViewModel room);
        RoomGalleryViewModel GetRoomGalleryById(int roomId);
        RoomImageViewModel GetRoomImageById(int imageId);
        void RemoveImage(int imageId);
        void AddRoomImage(AddImageViewModel roomImage);
    }
}
