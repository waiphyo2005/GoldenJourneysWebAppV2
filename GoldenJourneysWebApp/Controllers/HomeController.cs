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


        //Home Page (Not Login)
		public IActionResult Index()
        {
            return View();
        }


        //Home Page (Login)
        [Authorize(Roles = "Customer")]
        public IActionResult LoginHome()
        {
            return View();
        }


        //Privacy Page
        [Authorize(Roles = "Customer")]
        public IActionResult Privacy()
        {
            return View();
        }


        //Register Customer
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


        //Login
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

                            return RedirectToAction("LoginHome", "Home");
                        }
					}
				}
				ModelState.AddModelError(string.Empty, "Incorrect email or password.");
			}
			return View(loginUser);
		}


        //Access Denied
        public IActionResult AccessDenied()
        {
            return View();
        }


        //Logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        //Customer Profile
        [Authorize(Roles = "Customer")]
        public IActionResult CustomerProfile()
        {
            var user = _userService.GetProfileByEmail(HttpContext.User.Identity.Name);
            return View(user);
        }


        //Update Profile Details
        [Authorize(Roles = "Customer")]
        public IActionResult UpdateProfile(string email)
        {
            var user = _userService.GetProfileByEmail(email);
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


        //Reset Password
        public IActionResult ResetPassword(string email)
        {
            var user = _userService.GetUserForPasswordReset(email);
            return View(user);
        }
        [HttpPost]
        public IActionResult ResetPassword(string email, ResetPasswordViewModel user)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}
			_userService.UpdatePassword(email, user);
			TempData["Message"] = "Password has been Updated!";
			return RedirectToAction("CustomerProfile", "Home");
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
