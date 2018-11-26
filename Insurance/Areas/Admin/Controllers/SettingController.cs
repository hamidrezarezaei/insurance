using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public SettingController(repository repository)
        {
            this.repository = repository;
        }
        #endregion
        public IActionResult Index(int pageNumber, string searchString)
        {
            var settings = this.repository.GetSettings(pageNumber, searchString);
            ViewData["searchString"] = searchString;
            return View(settings);
        }


        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new setting());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(setting setting, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(setting);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(setting);
        }

        // GET: insurances/Edit/5
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = this.repository.GetSetting(id);
            if (setting == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, setting setting, string returnUrl = null)
        {
            if (id != setting.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(setting);

                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(setting);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = this.repository.GetSetting(id);
            if (setting == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(setting);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var setting = this.repository.GetSetting(id);
            this.repository.DeleteEntity(setting);
            return RedirectToLocal(returnUrl);
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