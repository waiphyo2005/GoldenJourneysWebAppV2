using GoldenJourneysWebApp.Models;

namespace GoldenJourneysWebApp.Repository
{
    public interface IBookingTourServices
    {
        List<UserAllToursViewModel> AllTours();
		TourDetailandBookingViewModel GetTourDetails(int tourId);
        bool isDateAvailable (TourDetailandBookingViewModel booking);
        bool isCapacityAvailable (TourDetailandBookingViewModel booking);
        string GetUserNameById(TourDetailandBookingViewModel booking);
        double CalculateTotalPrice(TourDetailandBookingViewModel booking);
        void BookTourCapacity(BookingConfirmationViewModel booking);
        void RecordBooking(BookingConfirmationViewModel booking);
        List<UserTourBookingsViewModel> GetBookingsById (int userId);
        TourBookingCancelViewModel GetBooking(int bookingId);
        void CancelBooking(int bookingId);
	}
}
