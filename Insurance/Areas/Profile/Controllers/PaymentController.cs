using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using ThirdParty;
using Insurance.Services;

namespace Insurance.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class PaymentController : Controller
    {

        #region Constructor
        private readonly repository repository;
        private readonly MellatService mellatService;
        private readonly HookManager hookManager;

        public PaymentController(repository repository, MellatService mellatService,HookManager hookManager)
        {
            this.repository = repository;
            this.mellatService = mellatService;
            this.hookManager = hookManager;
        }
        #endregion

        public IActionResult Index(int id)
        {
            var order = this.repository.GetOrder(id);
            if (order.paymentType.name == "online")
            {
                switch (this.repository.GetSetting("bank") )
                {
                    case "mellat":
                    default:
                    var res = this.mellatService.PayRequest(order);
                    return RedirectToAction("MellatRequest", "Payment", new { refId = res });
                }
            }
            else
            {
                //پرداخت نشده
                this.repository.ChangeOrderStatus(order,(int)orderStatuses.noPayment);
                this.hookManager.HookFired("orderCompleted", order.user, order);
                return View("Completed", order);
            }
        }
        public IActionResult MellatRequest(string refId)
        {
            ViewBag.jscode = "postRefId('" + refId + "')";
            return View();
        }
        [HttpPost]
        public IActionResult MellatResponse(string RefId="", string ResCode="", string SaleOrderId="", string SaleReferenceId="")
        {
            try
            {
                order order = this.repository.GetOrder(Int32.Parse(SaleOrderId));
                //repository.AddLogToOrder(order, "MellatResponse rescode=" + ResCode + "&saleorderid=" + SaleOrderId + "&SaleReferenceId=" + SaleReferenceId);

                if (ResCode != "0")
                    throw new Exception(Mellat.TranslateMessage(ResCode));

                this.mellatService.VerifyResult(order, SaleReferenceId);
                this.repository.SetOrderBankReference(order, SaleReferenceId);
                this.repository.ChangeOrderStatus(order, (int)orderStatuses.payed);

                //try
                //{
                //    this.mellatService.SettleRequest(order);
                //}
                //catch (Exception ex)
                //{
                //    //AddMessage(ex.Message, "Info");
                //}
                this.hookManager.HookFired("orderCompleted", order.user, order);
                this.hookManager.HookFired("paymentCompleted", order.user, order);
                return View("Completed",order);
            }
            catch(Exception ex)
            {
                return View("error",ex.Message);
            }
        }
    }
}