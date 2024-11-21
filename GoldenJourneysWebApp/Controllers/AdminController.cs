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
        [Authorize]
        public IActionResult Index()
        {
            List<UserViewModel> users = _userService.GetUsers();
            return View(users);
        }
		public IActionResult FilterUsers(string Type)
		{
			List<UserViewModel> users = _userService.FilterUsers(Type);
			return View(users);
		}
		[Authorize]
		public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAdmin(AdminCreateViewModel admin) 
        {
            if (ModelState.IsValid)
            {
                bool usedEmail = _userService.IsEmailUnique(admin.Email);
                if (!usedEmail)
                {
                    _userService.AdminCreate(admin);
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError(string.Empty, "Account already exist! Please use different Email.");
            }            
            return View(admin);
        }

        
		[Authorize]
		public IActionResult EditUser(string email)
        {
            UserViewModel user = _userService.GetUserByEmail(email);
            return View(user);
        }
        [HttpPost]
        public IActionResult EditUser(string email, UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _userService.UserEdit(email, user);
            return RedirectToAction("Index", "Admin");
        }

	}
}
