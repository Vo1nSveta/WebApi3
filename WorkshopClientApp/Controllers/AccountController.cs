using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkshopClientApp.Models;

namespace WorkshopClientApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model.Password != "admin" && model.Username != "admin")
            {
                ModelState.AddModelError("incorrectPassword", "Неверный логин или пароль");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            //Связка логин-пароь верная
            await Authenticate("admin");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();


            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var identity = new ClaimsIdentity(claims, scheme);
            await HttpContext.SignInAsync(scheme, new ClaimsPrincipal(identity));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
