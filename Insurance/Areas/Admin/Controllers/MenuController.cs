using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MenuController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public MenuController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index()
        {
            var menus = this.repository.GetMenus_hierarchy();
            return View(menus);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = this.repository.GetMenu(id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }
        public IActionResult Create(string returnUrl = null)
        {
            ViewBag.menus = new SelectList(this.repository.GetMenus(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(new menu());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(menu menu, IFormFile image, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(menu, image);
                return RedirectToLocal(returnUrl);
            }

            ViewBag.menus = new SelectList(this.repository.GetMenus(), "id", "title", menu.fatherId);
            ViewData["ReturnUrl"] = returnUrl;
            return View(menu);
        }

        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }
            var menu = this.repository.GetMenu(id);
            ViewBag.menus = new SelectList(this.repository.GetMenus(), "id", "title", menu.fatherId);

            if (menu == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, menu menu, IFormFile image, string returnUrl = null)
        {
            if (id != menu.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(menu, image);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(menu);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = this.repository.GetMenu(id);
            if (menu == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(menu);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var menu = this.repository.GetMenu(id);
            this.repository.DeleteEntity(menu);
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