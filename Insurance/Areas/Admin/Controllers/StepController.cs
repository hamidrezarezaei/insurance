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
    public class StepController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public StepController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Details(int? id, int pageNumber, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = this.repository.GetStep(id); ;


            if (step == null)
            {
                return NotFound();
            }
            ViewBag.fieldSets = this.repository.GetFieldSetsOfStep(id, pageNumber, searchString);
            ViewData["searchString"] = searchString;
            return View(step);
        }

        public IActionResult Create(int insuranceId, string returnUrl = null)
        {
            ViewData["ParentId"] = insuranceId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(new step());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(step step, IFormFile image, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
               this.repository.AddEntity(step, image);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ParentId"] = step.insuranceId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(step);
        }
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = this.repository.GetStep(id);

            if (step == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(step);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, step step, IFormFile image, string returnUrl = null)
        {
            if (id != step.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(step, image);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(step);
        }

        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = this.repository.GetStep(id);
            if (step == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(step);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var step = this.repository.GetStep(id);
            this.repository.DeleteEntity(step);
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