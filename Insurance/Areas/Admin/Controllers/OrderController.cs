using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public OrderController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index(int? id)
        {
            ViewBag.orderStatuses = this.repository.GetActiveOrderStatuses();
            if (id == null)
                id = (int)orderStatuses.payed;
            ViewBag.orderStatusId = id;
            var orders = this.repository.GetOrdersInStatus_ThisSite(id);
            return View(orders);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = this.repository.GetOrder(id); 
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
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