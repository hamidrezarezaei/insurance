using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PaymentTypeController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public PaymentTypeController(repository repository)
        {
            this.repository = repository;
        }
        #endregion
        public IActionResult Index(int pageNumber, string searchString)
        {
            var paymentTypes = this.repository.GetPaymentTypes(pageNumber, searchString);
            ViewData["searchString"] = searchString;
            return View(paymentTypes);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = this.repository.GetPaymentType(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new paymentType());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(paymentType paymentType, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(paymentType);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(paymentType);
        }

        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = this.repository.GetPaymentType(id);
            if (paymentType == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(paymentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, paymentType paymentType, string returnUrl = null)
        {
            if (id != paymentType.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(paymentType);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(paymentType);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = this.repository.GetPaymentType(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(paymentType);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var paymentType = this.repository.GetPaymentType(id);
            this.repository.DeleteEntity(paymentType);
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