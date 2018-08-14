using DAL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;
using Insurance.Services;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DataValueController : Controller
    {
        #region Constructor

        private readonly repository repository;

        public DataValueController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Create(int dataTypeId, string returnUrl = null)
        {
            dataValue_vm dataValue_vm = new dataValue_vm
            {
                dataValue = new dataValue(),
                terms = this.repository.GetTermsIncludeCategory(dataTypeId),
                selectedCategories = new List<int>()
            };

            ViewBag.dataValues = new SelectList(this.repository.GetDataValuesOfFatherDataType(dataTypeId).OrderBy(dv => dv.title), "id", "title");

            ViewData["ParentId"] = dataTypeId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(dataValue_vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(dataValue_vm dataValue_vm, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var dv = this.repository.AddEntity(dataValue_vm.dataValue);
                this.repository.SetCategoriesForDataValue(dv.id, dataValue_vm.selectedCategories);
                return RedirectToLocal(returnUrl);
            }
            ViewBag.dataValues = new SelectList(this.repository.GetDataValuesOfFatherDataType(dataValue_vm.dataValue.dataTypeId).OrderBy(dv => dv.title), "id", "title");

            ViewData["ParentId"] = dataValue_vm.dataValue.dataTypeId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(dataValue_vm);
        }
        public IActionResult Edit(int? id,string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataValue = this.repository.GetDataValue(id);
            if (dataValue == null)
            {
                return NotFound();
            }

            dataValue_vm dataValue_vm = new dataValue_vm
            {
                dataValue = dataValue,
                terms = this.repository.GetTermsIncludeCategory(dataValue.dataTypeId),
                selectedCategories = this.repository.GetAcitveCategories(id).Select(c => c.id).ToList()
            };

            ViewBag.dataValues = new SelectList(this.repository.GetDataValuesOfFatherDataType(dataValue.dataTypeId).OrderBy(dv => dv.title), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(dataValue_vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, dataValue_vm dataValue_vm, string returnUrl = null)
        {
            if (id != dataValue_vm.dataValue.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(dataValue_vm.dataValue);
                this.repository.SetCategoriesForDataValue(id, dataValue_vm.selectedCategories);
                return RedirectToLocal(returnUrl);
            }
            ViewBag.dataValues = new SelectList(this.repository.GetDataValuesOfFatherDataType(dataValue_vm.dataValue.dataTypeId).OrderBy(dv => dv.title), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(dataValue_vm);
        }

        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataValue = this.repository.GetDataValue(id);
            if (dataValue == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(dataValue);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var dataValue = this.repository.GetDataValue(id);
            this.repository.DeleteEntity(dataValue);
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