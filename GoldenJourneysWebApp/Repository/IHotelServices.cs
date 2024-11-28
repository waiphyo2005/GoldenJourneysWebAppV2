using GoldenJourneysWebApp.Models;

namespace GoldenJourneysWebApp.Repository
{
    public interface IHotelServices
    {
        List<HotelViewModel> GetHotels();
        List<HotelViewModel> FilteredHotels(string Status);

		void CreateHotel(CreateHotelViewModel hotel);
        bool ValidateHotelName(CreateHotelViewModel hotel);
        HotelDetailViewModel GetHotelById (int id);
    }
}
