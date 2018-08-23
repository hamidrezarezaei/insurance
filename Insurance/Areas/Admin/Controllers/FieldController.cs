using Entities;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Insurance.Services;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FieldController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public FieldController(repository repository)
        {
            this.repository = repository;
        }
        #endregion


        public IActionResult Create(int fieldSetId, string returnUrl = null)
        {
            var fieldSet = this.repository.GetFieldSet(fieldSetId);
            var step = this.repository.GetStep(fieldSet.stepId);
            ViewBag.fields = new SelectList(this.repository.GetFieldsOfInsurance(step.insuranceId), "id", "titleAndType");
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes().OrderBy(f => f.title).ToList(), "id", "title");
            ViewData["ParentId"] = fieldSetId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(new field());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(field field, IFormFile image, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddEntity(field, image);
                return RedirectToLocal(returnUrl);
            }

            var fieldSet = this.repository.GetFieldSet(field.fieldSetId);
            var step = this.repository.GetStep(fieldSet.stepId);
            ViewBag.fields = new SelectList(this.repository.GetFieldsOfInsurance(step.insuranceId), "id", "titleAndType");
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes().OrderBy(f => f.title).ToList(), "id", "title");
            ViewData["ParentId"] = field.fieldSetId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(field);
        }
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var field = this.repository.GetField(id);
            var fieldSet = this.repository.GetFieldSet(field.fieldSetId);
            var step = this.repository.GetStep(fieldSet.stepId);
            ViewBag.fields = new SelectList(this.repository.GetFieldsOfInsurance(step.insuranceId), "id", "titleAndType");
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes().OrderBy(f => f.title).ToList(), "id", "title");

            if (field == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(field);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, field field, IFormFile image, string returnUrl = null)
        {
            if (id != field.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(field, image);
                return RedirectToLocal(returnUrl);
            }
            var fieldSet = this.repository.GetFieldSet(field.fieldSetId);
            var step = this.repository.GetStep(fieldSet.stepId);
            ViewBag.fields = new SelectList(this.repository.GetFieldsOfInsurance(step.insuranceId), "id", "titleAndType");
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes().OrderBy(f => f.title).ToList(), "id", "title");

            ViewData["ReturnUrl"] = returnUrl;
            return View(field);
        }

        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var field = this.repository.GetField(id);
            if (field == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(field);
        }

        // POST: fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var field = this.repository.GetField(id);
            this.repository.DeleteEntity(field);
            return RedirectToLocal(returnUrl);
        }

        public IActionResult Duplicate(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var field = this.repository.GetField(id);
            if (field == null)
            {
                return NotFound();
            }

            this.repository.DuplicateEntity(field,new field());

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