using System;
using System.Collections.Generic;
using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ThirdParty;

namespace Insurance.Controllers
{
    [Route("api/Order")]
    public class OrderController : Controller
    {
        #region Constructor
        private readonly IHostingEnvironment env;
        //context ctx;
        repository repository;
        private readonly HookManager hookManager;

        public OrderController(repository repository, HookManager hookManager, IHostingEnvironment env)
        {
            //this.ctx = ctx;
            this.repository = repository;
            this.hookManager = hookManager;
            this.env = env;
        }
        #endregion

        [HttpPost("ProcessOrder")]
        public int ProcessOrder([FromBody]insurance_client insurance)
        {
            //اگر قبلا رکورد این سفارش ثبت شده همان را بردار
            if (insurance.orderId > 0)
            {
                order order = this.repository.GetOrder(insurance.orderId);
                this.repository.MapInsuranceToOrder(order,insurance);
                return order.id;
            }
            //اگر سفارش جدید است برای آن رکورد جدید ایجاد کن
            //باید چک شود که لاگین هستیم یا نه
            //else if (this.repository.GetUserId() > 0)
            else 
            {
                int orderId = repository.AddOrder(insurance);
                return orderId;
            }
            //از اینجا به بعد مال قبله
            //int orderId = repository.AddOrder(insurance);
            ////چون یوزر را میخواهیم مجبوریم این کار را بکنیم
            //order order = this.repository.GetOrder(orderId);
            //this.hookManager.HookFired("orderAdded", order.user, order);
            //return orderId;
            return 0;
        }

        [HttpPost("AddOrder")]
        public int AddOrder([FromBody]insurance_client insurance)
        {
            int orderId = repository.AddOrder(insurance);
            //چون یوزر را میخواهیم مجبوریم این کار را بکنیم
            order order = this.repository.GetOrder(orderId);
            this.hookManager.HookFired("orderAdded", order.user, order);
            return orderId;
        }

        [HttpPost("uploadOrderFiles")]
        public int uploadOrderFiles()
        {
            try
            {
                var files = Request.Form.Files;
                var orderId = this.repository.ProccessOrderImage(files);
                return orderId;
            }
            catch (Exception ex)
            {
                return -1;
                //return ex.Message;
            }
        }

        [HttpGet("[action]")]
        public List<paymentType_client> GetPaymentTypes()
        {
            return repository.GetAcitvePaymentTypesOfCurrentUser_Client();
        }

    }
}