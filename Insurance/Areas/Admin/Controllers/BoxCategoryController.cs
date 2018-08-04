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
    public class BoxCategoryController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public BoxCategoryController(repository repository)
        {
            this.repository = repository;
        }
        #endregion
        public IActionResult Index()
        {
            var boxCategories = this.repository.GetBoxCategories_hierarchy();
            return View(boxCategories);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boxCategory = this.repository.GetBoxCategory(id);
            if (boxCategory == null)
            {
                return NotFound();
            }

            ViewBag.boxes = this.repository.GetBoxesOfBoxCategory(id);
            return View(boxCategory);
        }

        public IActionResult Create(string returnUrl = null)
        {
            ViewBag.boxCategories = new SelectList(this.repository.GetBoxCategories(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(new boxCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(boxCategory boxCategory, IFormFile image, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(boxCategory, image);
                return RedirectToLocal(returnUrl);
            }
            ViewBag.boxCategories = new SelectList(this.repository.GetBoxCategories(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(boxCategory);
        }
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boxCategory = this.repository.GetBoxCategory(id);

            if (boxCategory == null)
            {
                return NotFound();
            }
            ViewBag.boxCategories = new SelectList(this.repository.GetBoxCategories(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(boxCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, boxCategory boxCategory, IFormFile image, string returnUrl = null)
        {
            if (id != boxCategory.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(boxCategory, image);
                return RedirectToLocal(returnUrl);
            }
            ViewBag.boxCategories = new SelectList(this.repository.GetBoxCategories(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(boxCategory);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boxCategory = this.repository.GetBoxCategory(id);
            if (boxCategory == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(boxCategory);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var boxCategory = this.repository.GetBoxCategory(id);
            this.repository.DeleteEntity(boxCategory);
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