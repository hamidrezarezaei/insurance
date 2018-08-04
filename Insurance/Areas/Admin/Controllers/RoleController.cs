using DAL;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RoleController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public RoleController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index()
        {
            return View(this.repository.GetAllRoles_ThisSite());
        }

        public IActionResult Create( string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name, string returnUrl = null)
        {
            var result = this.repository.AddRole(name);
            ViewData["ReturnUrl"] = returnUrl;
            return RedirectToAction("Index");
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