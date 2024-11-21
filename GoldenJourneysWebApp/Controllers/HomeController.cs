using System.Diagnostics;
using System.Security.Claims;
using GoldenJourneysWebApp.Models;
using GoldenJourneysWebApp.Repository;
using Microsoft.AspNetCore.Authentication;
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
        [Authorize]
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
					return RedirectToAction("Login", "Home");
				}
				ModelState.AddModelError(string.Empty, "Account already exist! Please use different Email.");
			}
			return View(customer);
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(string Email, string Password)
		{
			if (ModelState.IsValid)
			{
				bool Validity = _userService.ValidateUser(Email, Password);
				if (Validity)
				{
					var user = _userService.GetUserByEmail(Email);

					// Create claims based on the user data
					var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),          // Store user's email
                new Claim("UserType", user.Type)                  // Store user type (Admin, Customer, etc.)
            };

					// Create the user's identity
					var identity = new ClaimsIdentity(claims, "CookieAuthentication");
					var principal = new ClaimsPrincipal(identity);

					// Sign in the user
					await HttpContext.SignInAsync("CookieAuthentication", principal);

					// Redirect based on user type
					if (user.Type == "Admin")
					{
						return RedirectToAction("Index", "Admin");  // Redirect to Admin panel
					}

					// Redirect to Home page for normal users
					return RedirectToAction("Index", "Home");
				}

				// If the user credentials are incorrect
				ModelState.AddModelError(string.Empty, "Incorrect email or password.");
			}

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
