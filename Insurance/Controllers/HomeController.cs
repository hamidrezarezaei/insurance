using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        private readonly repository repository;
        private readonly HookManager hookManager;


        public HomeController(repository repository, HookManager hookManager)
        {
            this.repository = repository;
            this.hookManager = hookManager;
        }

        public IActionResult Index()
        {
            this.ManageRemindersAsync();

            ViewData["Title"] = this.repository.GetSetting("siteTitle");
            ViewData["MetaDescription"] = this.repository.GetSetting("MetaDescription");
            ViewData["MetaKeywords"] = this.repository.GetSetting("MetaKeywords");
            ViewData["GoogleAnalytics"] = this.repository.GetSetting("GoogleAnalytics");

            return View(this.viewAddress("Index"));
        }

        public IActionResult Post(int id, string title)
        {
            var post = this.repository.GetPost(id);
            ViewData["Title"] = post.title;
            ViewData["MetaDescription"] = post.metaDescription;
            ViewData["MetaKeywords"] = post.metaKeywords;
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

        private async Task ManageRemindersAsync()
        {
            //اولین نفری که در هر روز به سایت وارد شد باعث می شود که تمام یاد آوری های مال اون روز ارسال شوند
            if (this.repository.GetLastAccess().Day == DateTime.Now.Day)
                return;
            PersianCalendar pc = new PersianCalendar();
            if (pc.GetHour(DateTime.Now) < 7)
                return;

            this.repository.UpdateLastAccess();
            var reminders = this.repository.GetReminders(7).ToList();
            this.hookManager.HookFired("reminder7", reminders);

            reminders = this.repository.GetReminders(3).ToList();
            this.hookManager.HookFired("reminder3", reminders);

            reminders = this.repository.GetReminders(1).ToList();
            this.hookManager.HookFired("reminder1", reminders);

        }
        #endregion
    }
}
