using System.Diagnostics;
using System.Security.Claims;
using System.Web.Helpers;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoldenJourneysWebApp.Controllers
{
    public class HomeController : Controller
    {
		private IUserService _userService;
		public HomeController(IUserService userService)
		{
			_userService = userService;
		}
		public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Customer")]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult RegisterCustomer()
		{
			return View();
		}
		[HttpPost]
		public IActionResult RegisterCustomer(CustomerRegistrationViewModel customer)
		{
			if (ModelState.IsValid)
			{
				bool usedEmail = _userService.IsEmailUnique(customer.Email);
				if (!usedEmail)
				{
					_userService.CustomerRegister(customer);
					return RedirectToAction("RegisterSuccess", "Home", new { userEmail = customer.Email});
				}
				ModelState.AddModelError(string.Empty, "Account already exist! Please use different Email.");
			}
			return View(customer);
		}
		public IActionResult RegisterSuccess(string userEmail)
		{
			var user = _userService.GetUserByEmail(userEmail);
			return View(user);
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(LoginViewModel loginUser)
		{
			if (ModelState.IsValid)
			{
				bool Validity = _userService.ValidateUser(loginUser);
				if (Validity)
				{
					var user = _userService.GetUserByEmail(loginUser.Email);
					if (user != null)
					{
						if(user.Type == "Admin")
						{
							var claims = new List<Claim>
							{
                                new Claim(ClaimTypes.Name, user.Email),
                                new Claim(ClaimTypes.Role, user.Type),

                            };

							var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
							HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

							return RedirectToAction("Index", "Admin");
						}
						if (user.Type == "Customer")
						{
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Email),
                                new Claim(ClaimTypes.Role, user.Type),

                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            return RedirectToAction("Privacy", "Home");
                        }
					}
				}
				ModelState.AddModelError(string.Empty, "Incorrect email or password.");
			}
			return View(loginUser);
		}
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Customer")]
        public IActionResult CustomerProfile()
        {
            AccountProfileViewModel user = _userService.GetProfileByEmail(HttpContext.User.Identity.Name);
            return View(user);
        }

        //Update Profile Details
        [Authorize(Roles = "Customer")]
        public IActionResult UpdateProfile(string email)
        {
            AccountProfileViewModel user = _userService.GetProfileByEmail(email);
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateProfile(string email, AccountProfileViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _userService.UpdateAccount(email, user);
            TempData["Message"] = "Account has been Updated!";
            return RedirectToAction("CustomerProfile", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
