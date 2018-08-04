using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FieldSetController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public FieldSetController(repository repository)
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

            var fieldSet = this.repository.GetFieldSet(id);

            if (fieldSet == null)
            {
                return NotFound();
            }
            ViewBag.fields = this.repository.GetFieldsOfFieldSet(id);
            return View(fieldSet);
        }
        public IActionResult Create(int stepId, string returnUrl = null)
        {
            ViewData["ParentId"] = stepId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(new fieldSet());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(fieldSet fieldSet, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddEntity(fieldSet);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ParentId"] = fieldSet.stepId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(fieldSet);
        }
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldSet = this.repository.GetFieldSet(id);

            if (fieldSet == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(fieldSet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, fieldSet fieldSet, string returnUrl = null)
        {
            if (id != fieldSet.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(fieldSet);
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(fieldSet);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldSet = this.repository.GetFieldSet(id);
            if (fieldSet == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(fieldSet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var fieldSet = this.repository.GetFieldSet(id);
            this.repository.DeleteEntity(fieldSet);
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