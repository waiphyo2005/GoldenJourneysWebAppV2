using GoldenJourneysWebApp.Models;

namespace GoldenJourneysWebApp.Repository
{
    public interface IBookingHotelServices
    {
       List<UserAllHotelsViewModel> GetAllHotels();
        UserHotelDetailsViewModel GetHotelDetails(int hotelId);
        UserRoomViewandBookingModel GetRoomById(int roomId);
        bool isDateAvailable (UserRoomViewandBookingModel booking);
        bool isQtyAvailable (UserRoomViewandBookingModel booking);
        string GetUserNameById(UserRoomViewandBookingModel booking);
        double CalculateTotalPrice(UserRoomViewandBookingModel booking);
        void BookRoomQty(HBookingConfirmationViewModel booking);
        void RecordBooking(HBookingConfirmationViewModel booking);
    }
}
