using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostCategoryController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public PostCategoryController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index()
        {
            var postCategories = this.repository.GetPostCategories();
            return View(postCategories);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = this.repository.GetPostCategory(id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new postCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(postCategory postCategory, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(postCategory);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(postCategory);
        }

        // GET: insurances/Edit/5
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = this.repository.GetPostCategory(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(postCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, postCategory postCategory, string returnUrl = null)
        {
            if (id != postCategory.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(postCategory);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(postCategory);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = this.repository.GetPostCategory(id);
            if (postCategory == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(postCategory);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var postCategory = this.repository.GetPostCategory(id);
            this.repository.DeleteEntity(postCategory);
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