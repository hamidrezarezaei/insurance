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
    public class InsuranceController : Controller
    {
        #region Constructor
        private readonly repository repository;
        public InsuranceController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index()
        {
            var insurances = this.repository.GetInsurances();
            return View(insurances);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = this.repository.GetInsurance(id);
            if (insurance == null)
            {
                return NotFound();
            }

            ViewBag.steps = this.repository.GetStepsOfInsurance(id);
            return View(insurance);
        }

        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new insurance());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(insurance insurance, IFormFile image, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(insurance, image);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(insurance);
        }

        // GET: insurances/Edit/5
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = this.repository.GetInsurance(id);
            if (insurance == null)
            {
                return NotFound();
            }
            ViewBag.steps = this.repository.GetStepsOfInsurance(id);

            ViewData["ReturnUrl"] = returnUrl;
            return View(insurance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, insurance insurance, IFormFile image, string returnUrl = null)
        {
            if (id != insurance.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(insurance, image);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(insurance);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = this.repository.GetInsurance(id);
            if (insurance == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(insurance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var insurance = this.repository.GetInsurance(id);
            this.repository.DeleteEntity(insurance);
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