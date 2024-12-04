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
		private IBookingTourServices _bookingTourServices;
		private IBookingHotelServices _bookingHotelServices;
		public HomeController(IUserService userService, IBookingTourServices bookingTourServices, IBookingHotelServices bookingHotelServices)
		{
			_userService = userService;
			_bookingTourServices = bookingTourServices;
			_bookingHotelServices = bookingHotelServices;
		}


        //Home Page (Not Login)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        //Privacy Page
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }


        //Register Customer
        [HttpGet]
        public IActionResult RegisterCustomer()
		{
			return View();
		}

		[HttpPost]
		public IActionResult RegisterCustomer(CustomerRegistrationViewModel customer)
		{
            if(customer.Password != customer.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Two Passwords doesn't match.");
            }
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
        [HttpGet]
        public IActionResult RegisterSuccess(string userEmail)
		{
			var user = _userService.GetUserByEmail(userEmail);
			return View(user);
		}


        //Login
        [HttpGet]
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
                                new Claim(ClaimTypes.Name, user.Id.ToString()),
                                new Claim(ClaimTypes.Role, user.Type)
                            };

							var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
							HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

							return RedirectToAction("Index", "Admin");
						}
						if (user.Type == "Customer")
						{
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Id.ToString()),
                                new Claim(ClaimTypes.Role, user.Type)
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            return RedirectToAction("LoginHome", "Customer");
                        }
					}
				}
				ModelState.AddModelError(string.Empty, "Incorrect email or password.");
			}
			return View(loginUser);
		}


        //Access Denied
        [HttpGet]
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

		//View Available Tours
		public IActionResult DisplayTourPackages()
		{
			var allTours = _bookingTourServices.AllTours();
			return View(allTours);
		}

		//View Available Hotels
		public IActionResult DisplayHotels()
		{
			var allHotels = _bookingHotelServices.GetAllHotels();
			return View(allHotels);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
