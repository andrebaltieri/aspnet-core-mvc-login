using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DemoLogin.Models;
using DemoLogin.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DemoLogin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index([FromQuery]string returnUrl)
        {
            Console.Write(returnUrl);
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, [Bind("Email,Password")] UserModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = UserRepository.Get(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Usuario Não Encontrado");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            string returnUrl = TempData["ReturnUrl"]?.ToString();
            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
