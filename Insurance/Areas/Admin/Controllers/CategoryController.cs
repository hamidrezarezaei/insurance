using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public CategoryController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index(int? id)
        {
            if (id == null)
                return NotFound();
            var categories = this.repository.GetCategoriesOfTerm(id);
            if (categories  == null)
            {
                return NotFound();
            }
            return View(categories);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = this.repository.GetCategory(id); ;
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.attributes = this.repository.GetAttributesOfCategory(id);

            return View(category);
        }

        public IActionResult Create(int termId, string returnUrl = null)
        {
            ViewData["ParentId"] = termId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(new category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(category category, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                category = (category)this.repository.AddEntity(category);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ParentId"] = category.termId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(category);
        }

        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = this.repository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, category category, string returnUrl = null)
        {
            if (id != category.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(category);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(category);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = this.repository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var category = this.repository.GetCategory(id);
            this.repository.DeleteEntity(category);
                return RedirectToLocal(returnUrl);
            //return RedirectToAction("Details", this.ParentControllerName, new { Area = this.AreaName, id = category.termId });
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