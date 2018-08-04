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
    public class SmsController : Controller
    {
        #region Constructor
        private readonly repository repository;
        public SmsController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

       
        public IActionResult Create(int hookId, string returnUrl = null)
        {
            ViewData["ParentId"] = hookId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(new sms());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(sms sms, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddEntity(sms);
                return RedirectToLocal(returnUrl);
            }

            ViewData["ParentId"] = sms.hookId;
            ViewData["ReturnUrl"] = returnUrl;
            return View(sms);
        }

        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sms = this.repository.GetSms(id);
            if (sms == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(sms);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, sms sms, string returnUrl = null)
        {
            if (id != sms.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(sms);
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(sms);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sms = this.repository.GetSms(id);
            if (sms == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(sms);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var sms = this.repository.GetSms(id);
            this.repository.DeleteEntity(sms);
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