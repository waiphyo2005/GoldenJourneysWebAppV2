using GoldenJourneysWebApp.Data.Entities;
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
        GalleryViewModel GetTourGallery(int Id);
        TourGalleryViewModel GetImage(int Id);
        void RemoveImage(int id);
        void AddTourImages(AddImageViewModel image);
        List<TourAvailability> GetAvailabilitySlots(int tourId);
        bool CheckDuplicateDate(AddAvailabilityViewModel slot);
        void AddAvailableSlot(AddAvailabilityViewModel slot);
        EditAvailabilityViewModel GetAvailableSlot(int id);
        void EditCapacity(EditAvailabilityViewModel slot);

    }
}
