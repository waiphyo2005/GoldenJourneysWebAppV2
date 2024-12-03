using System.Security.Claims;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoldenJourneysWebApp.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        //View Users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            List<UserViewModel> users = _userService.GetUsers();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult FilterUsers(string Type)
		{
			List<UserViewModel> users = _userService.FilterUsers(Type);
			return View(users);
		}


        //Create New Admin
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAdmin(AdminCreateViewModel admin) 
        {
            if(admin.Password != admin.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Two Passwords doesn't match!");
            }
            if (ModelState.IsValid)
            {
                bool usedEmail = _userService.IsEmailUnique(admin.Email);
                if (!usedEmail)
                {
                    _userService.AdminCreate(admin);
                    return RedirectToAction("CreationSuccess", "Admin", new {adminEmail = admin.Email});
                }
                ModelState.AddModelError(string.Empty, "Account already exist! Please use different Email.");
            }            
            return View(admin);
        }

        public IActionResult CreationSuccess(string adminEmail)
        {
            var admin = _userService.GetUserByEmail(adminEmail);
            return View(admin);
        }


        //Edit User Details
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditUser(int userId)
        {
            UserViewModel user = _userService.GetUserById(userId);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            TempData["Message"] = "Record has been Updated!";
            _userService.UserEdit(user);
            return RedirectToAction("Index", "Admin");
        }


        //Admin Profile
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminProfile()
        {
            var adminId = Convert.ToInt32(HttpContext.User.Identity.Name);
			UserViewModel admin = _userService.GetUserById(adminId);
            return View(admin);
        }


        //Reset Password
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminResetPassword(int userId)
        {
            var user = _userService.GetUserForPasswordReset(userId);
            return View(user);
        }
        [HttpPost]
        public IActionResult AdminResetPassword(ResetPasswordViewModel user)
        {
            if (user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Two Passwords doesn't match!");
            }
            if (ModelState.IsValid)
            {
                _userService.UpdatePassword(user);
                TempData["Message"] = "Password has been Updated!";
                return RedirectToAction("AdminProfile", "Admin");
            }
            return View(user);
        }

    }
}
