using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore2Boilerplate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCore2Boilerplate.ViewModels;
using BoilerplateData.Work.Interface;
using System.Net;
using BoilerplateData.DTOs;

namespace AspNetCore2Boilerplate.Controllers
{
    public class UserController : BaseController
    {
        private IUnitOfWork work;

        public UserController(IUnitOfWork work)
        {
            this.work = work;
        }

        [HttpGet, Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Authorize]
        public IActionResult Edit()
        {
            var model = new UserEditViewModel();
            model.EmailAddress = CurrentUser.EmailAddress;
            return View(model);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!work.UserRepository.CheckUniqueEmail(model.EmailAddress))
            {
                ModelState.AddModelError("Unique", "Email Already Exists");
                return View(model);
            }
            work.UserRepository.EditEmail(CurrentUser.ID, model.EmailAddress);
            work.Save();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Register/")]
        public IActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        [HttpPost("Register/")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserRegisterViewModel model)
        {
            if (!work.UserRepository.CheckUniqueUsername(model.Username)) {
                ModelState.AddModelError("Username", "Username already exists.");
            }
            if (!work.UserRepository.CheckUniqueEmail(model.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email Address already exists.");
            }
            if (ModelState.IsValid)
            {
                work.UserRepository.Register(model.Username, model.EmailAddress, model.Password);
                work.Save();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet("Login/")]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost("Login/")]
        public IActionResult Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = work.UserRepository.Login(model.EmailAddressOrUsername, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("Invalid Login", "Invalid Credentials");
                return View(model);
            }

            var ClaimsPrincipal = ApplicationUser.SetClaims(user);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ClaimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                });

            string decodedUrl = string.Empty;
            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                decodedUrl = WebUtility.HtmlDecode(model.ReturnUrl);
                if (Url.IsLocalUrl(decodedUrl))
                {
                    return this.Redirect(decodedUrl);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
