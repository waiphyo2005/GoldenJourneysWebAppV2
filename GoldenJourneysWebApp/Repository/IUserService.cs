using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;

namespace GoldenJourneysWebApp.Repository
{
    public interface IUserService
    {
        List<UserViewModel> GetUsers();
        List<UserViewModel> FilterUsers(string userType);
        bool IsEmailUnique(string email);
        void AdminCreate(AdminCreateViewModel admin);
        void CustomerRegister(CustomerRegistrationViewModel customer);
        UserViewModel GetUserByEmail(string userEmail);
        void UserEdit(string email, UserViewModel user);
        bool ValidateUser(string Email, string Password);
    }
}
