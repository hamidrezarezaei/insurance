using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DataTypeController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public DataTypeController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index()
        {
            var dataTypes = this.repository.GetDataTypes();
            return View(dataTypes);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataType = this.repository.GetDataType(id);
            if (dataType == null)
            {
                return NotFound();
            }
            ViewBag.dataValues = this.repository.GetDataValuesOfDataType(id);
            return View(dataType);
        }
        public IActionResult Create(string returnUrl = null)
        {
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(new dataType());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(dataType dataType, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                dataType = (dataType)repository.AddEntity(dataType);
                return RedirectToLocal(returnUrl);
            }
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(dataType);
        }
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataType = this.repository.GetDataType(id);
            if (dataType  == null)
            {
                return NotFound();
            }
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(dataType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, dataType dataType, string returnUrl = null)
        {
            if (id != dataType.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                dataType = (dataType)this.repository.UpdateEntity(dataType);
                return RedirectToLocal(returnUrl);
            }
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(dataType);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataType = this.repository.GetDataType(id);
            if (dataType  == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(dataType);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var dataType = this.repository.GetDataType(id);
            this.repository.DeleteEntity(dataType);
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