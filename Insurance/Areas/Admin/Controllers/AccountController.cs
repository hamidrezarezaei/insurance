using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]

    public class AccountController : Controller
    {
        private readonly repository repository;

        public AccountController(repository repository)
        {
            this.repository = repository;
        }
        public IActionResult Login(string returnUrl = null)
        {
            this.repository.LogOut();
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Login(user_express user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = repository.Login(user.actualUserName, user.password, user.rememberMe);
                if (result.Succeeded)
                    return RedirectToLocal(returnUrl);
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(user_express user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = this.repository.AddUser(user);
                if (result.Succeeded)
                {
                    this.repository.Login(user.actualUserName, user.password, true);
                    return RedirectToLocal(returnUrl);
                }
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            this.repository.LogOut();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}