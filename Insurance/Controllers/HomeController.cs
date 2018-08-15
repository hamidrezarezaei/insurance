using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Insurance.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Controllers
{
    public class HomeController : Controller
    {
        //context ctx;
        repository repository;

        public HomeController(repository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View(this.viewAddress("Index"));
        }

        public IActionResult Post(int id, string title)
        {
            var post = this.repository.GetPost(id);
            return View(this.viewAddress("Post"), post);
        }
        public IActionResult PostCategory(int id, string title)
        {
            return View(this.viewAddress("PostCategory"));
        }
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        #region Private
        private string viewAddress(string type)
        {
            string viewName = repository.GetSiteName();
            ViewData["viewName"] = viewName;
            ViewData["Title"] = this.repository.GetSetting("siteTitle");
            ViewData["MetaDescription"] = this.repository.GetSetting("MetaDescription");
            ViewData["MetaKeywords"] = this.repository.GetSetting("MetaKeywords");
            ViewData["GoogleAnalytics"] = this.repository.GetSetting("GoogleAnalytics");

            switch (type)
            {
                case "Index":
                    return "~/Views/" + viewName + "/Index.cshtml";
                case "Post":
                    return "~/Views/" + viewName + "/Post.cshtml";
                case "PostCategory":
                    return "~/Views/" + viewName + "/PostCategory.cshtml";

            }
            return "";
        }


        #endregion
    }
}
