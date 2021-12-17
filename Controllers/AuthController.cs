using EducationSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Controllers
{
    public class AuthController : Controller
    {
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { area = "", controller = "Home" });
            }

            ModelState.AddModelError("", "Provide valid credentials to login");
            return View(model);
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        #endregion

        #region Forgot Password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        #endregion

        #region Logout
        [HttpGet]
        public IActionResult Logout()
        {
            //TODO: Clear existing user session
            return RedirectToAction("Login", new { area = "", controller = "Auth" });
        }
        #endregion
    }
}
