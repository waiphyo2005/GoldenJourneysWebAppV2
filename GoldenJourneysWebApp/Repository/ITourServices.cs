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
        TourViewModel GetTourAllDetails(int Id);
        TourDetailEditViewModel GetTourDetails(int Id);
        void UploadTourDetails(TourDetailEditViewModel tour);
        List<TourGalleryModel> GetTourGallery(int Id);
        TourGalleryModel GetImage(int Id);
        void RemoveImage(int id);
        void AddTourImages(AddImageModel image);

    }
}
