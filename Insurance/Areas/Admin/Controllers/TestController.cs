using System;
using System.Net;
using System.Net.Mail;
using DAL;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class TestController : Controller
    {
        private readonly repository repository;
        private readonly SmsSender smsSender;
        private readonly EmailSender emailSender;

        public TestController(repository repository, SmsSender SmsSender,EmailSender emailSender)
        {
            this.repository = repository;
            smsSender = SmsSender;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Email()
        {
            var b = this.emailSender.SendEmail("bimebaz@gmail.com", "test" + DateTime.Now.ToString(),"this email is test");
            return View(b);
        }

        public IActionResult SmsUrl()
        {
            smsSender.SendSms("09132057232,09120155182", "test" + DateTime.Now.ToString());
            return View();
        }
    }
}