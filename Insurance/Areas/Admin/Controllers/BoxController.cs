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
    public class BoxController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public BoxController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var box = this.repository.GetBox(id); ;
            if (box == null)
            {
                return NotFound();
            }

            return View(box);
        }
        public IActionResult Create(int boxCategoryId, string returnUrl = null)
        {
            ViewData["ParentId"] = boxCategoryId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(new box());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(box box, IFormFile image, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddEntity(box, image);
                return RedirectToLocal(returnUrl);
            }

            ViewData["ParentId"] = box.boxCategoryId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(box);
        }

        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var box = this.repository.GetBox(id);
            if (box == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(box);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, box box, IFormFile image, string returnUrl = null)
        {
            if (id != box.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                box = (box)this.repository.UpdateEntity(box, image);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(box);
        }

        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var box = this.repository.GetBox(id);
            if (box == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(box);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var box = this.repository.GetBox(id);
            this.repository.DeleteEntity(box);
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