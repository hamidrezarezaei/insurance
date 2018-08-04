using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AttributeController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public AttributeController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Create(int categoryId, string returnUrl = null)
        {
            ViewData["ParentId"] = categoryId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(new attribute());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(attribute attribute, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddEntity(attribute);
                return RedirectToLocal(returnUrl);
            }

            ViewData["ParentId"] = attribute.categoryId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(attribute);
        }

        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribute = this.repository.GetAttribute(id);
            if (attribute == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(attribute);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, attribute attribute, string returnUrl = null)
        {
            if (id != attribute.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(attribute);
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(attribute);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribute = this.repository.GetAttribute(id);
            if (attribute == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(attribute);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var attribute = this.repository.GetAttribute(id);
            this.repository.DeleteEntity(attribute);
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