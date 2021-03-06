﻿using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TermController : Controller
    {
        #region Constructor
        private readonly repository repository;
        public TermController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index(int pageNumber, string searchString)
        {
            var terms = this.repository.GetTerms(pageNumber, searchString);
            ViewData["searchString"] = searchString;
            return View(terms);
        }
        public IActionResult Details(int? id, int pageNumber, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = this.repository.GetTerm(id);
            if (term == null)
            {
                return NotFound();
            }
            ViewBag.categories = this.repository.GetCategoriesOfTerm(id, pageNumber, searchString);
            ViewData["searchString"] = searchString;
            return View(term);
        }
        public IActionResult Create(string returnUrl = null)
        {
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(new term());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(term term, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(term);
                return RedirectToLocal(returnUrl);
            }
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(term);
        }
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = this.repository.GetTerm(id);
            if (term == null)
            {
                return NotFound();
            }
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(term);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, term term, string returnUrl = null)
        {
            if (id != term.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(term);
                return RedirectToLocal(returnUrl);
            }
            ViewBag.dataTypes = new SelectList(this.repository.GetDataTypes(), "id", "title");
            ViewData["ReturnUrl"] = returnUrl;
            return View(term);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = this.repository.GetTerm(id);
            if (term == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(term);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var term = this.repository.GetTerm(id);
            this.repository.DeleteEntity(term);
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