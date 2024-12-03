using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoldenJourneysWebApp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private IUserService _userService;
        public CustomerController (IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Home Page (Login)
        [HttpGet]
        public IActionResult LoginHome()
        {
            return View();
        }

        //Customer Profile

        [HttpGet]
        public IActionResult CustomerProfile()
        {
            var userId = Convert.ToInt32(HttpContext.User.Identity.Name);
            var user = _userService.GetProfileById(userId);
            return View(user);
        }


        //Update Profile Details
        [HttpGet]
        public IActionResult UpdateProfile(int userId)
        {
            var user = _userService.GetProfileById(userId);
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateProfile(AccountProfileViewModel user)
        {
            bool usedEmail = _userService.IsEmailUnique(user.Email);
            if (usedEmail)
            {
                ModelState.AddModelError("Email", "This email already has an account. Please use another email.");
            }
            if (ModelState.IsValid)
            {
                _userService.UpdateAccount(user);
                TempData["Message"] = "Account has been Updated!";
                return RedirectToAction("CustomerProfile", "Customer");
            }
            return View(user);
        }


        //Reset Password
        [HttpGet]
        public IActionResult ResetPassword(int userId)
        {
            var user = _userService.GetUserForPasswordReset(userId);
            return View(user);
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel user)
        {
            if (user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Two Passwords doesn't match.");
            }
            if (ModelState.IsValid)
            {
                _userService.UpdatePassword(user);
                TempData["Message"] = "Password has been Updated!";
                return RedirectToAction("CustomerProfile", "Customer");
            }
            return View(user);
        }
    }
}
