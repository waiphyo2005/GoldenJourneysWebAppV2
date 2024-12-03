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
        void UserEdit(UserViewModel user);
        bool ValidateUser(LoginViewModel loginUser);
        AccountProfileViewModel GetProfileById(int userId);
        void UpdateAccount(AccountProfileViewModel user);
        ResetPasswordViewModel GetUserForPasswordReset(int userId);
        void UpdatePassword(ResetPasswordViewModel user);
        UserViewModel GetUserById(int userId);
    }
}
