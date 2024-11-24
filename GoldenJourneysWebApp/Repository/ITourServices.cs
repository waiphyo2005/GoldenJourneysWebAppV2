using GoldenJourneysWebApp.Models;

namespace GoldenJourneysWebApp.Repository
{
    public interface ITourServices
    {
        List<TourViewModel> GetTours();
        List<TourViewModel> FilterTours(string Status);
        void UploadTourDetails(TourCreateViewModel tour);
        void UploadTourImages(TourCreateViewModel tour);
        bool ValidateTourName (TourCreateViewModel tour);
        void UploadAvailabilitySlots(TourCreateViewModel tour);
    }
}
