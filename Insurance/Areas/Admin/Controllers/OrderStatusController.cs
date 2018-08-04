using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize]
    public class OrderStatusController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public OrderStatusController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index()
        {
            var orderStatuses = this.repository.GetOrderStatuses();
            return View(orderStatuses);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = this.repository.GetOrderStatus(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new orderStatus());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(orderStatus orderStatus, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(orderStatus);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(orderStatus);
        }

        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = this.repository.GetOrderStatus(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(orderStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, orderStatus orderStatus, string returnUrl = null)
        {
            if (id != orderStatus.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(orderStatus);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(orderStatus);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = this.repository.GetOrderStatus(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(orderStatus);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var orderStatus = this.repository.GetOrderStatus(id);
            this.repository.DeleteEntity(orderStatus);
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