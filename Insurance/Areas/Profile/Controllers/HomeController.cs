using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Profile.Controllers
{
    [Area("Profile")]
    [Authorize]
    public class HomeController : Controller
    {
        #region Constructor
        private readonly string AreaName = "Admin";
        private readonly string ControllerName = "Insurance";
        private readonly repository repository;

        public HomeController(repository repository)
        {
            this.repository = repository;
        }
        #endregion
        public IActionResult Index()
        {
            var orders = this.repository.GetOrdersOfCurrentUser_ThisSite();
            var x = this.repository.GetSetting("bank");
            return View(orders);
        }
    }
}