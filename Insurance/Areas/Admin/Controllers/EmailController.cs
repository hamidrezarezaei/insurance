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
    public class EmailController : Controller
    {
        #region Constructor
        private readonly repository repository;
        public EmailController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Create(int hookId, string returnUrl = null)
        {
            ViewData["ParentId"] = hookId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(new email());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(email email, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddEntity(email);
                return RedirectToLocal(returnUrl);
            }

            ViewData["ParentId"] = email.hookId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(email);
        }

        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = this.repository.GetEmail(id);
            if (email == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(email);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, email email, string returnUrl = null)
        {
            if (id != email.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(email);
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(email);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = this.repository.GetEmail(id);
            if (email == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(email);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var email = this.repository.GetEmail(id);
            this.repository.DeleteEntity(email);
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