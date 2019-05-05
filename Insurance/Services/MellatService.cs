using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdParty;

namespace Insurance.Services
{
    public class MellatService
    {

        #region Constructor
        private readonly repository repository;
        private readonly IHttpContextAccessor httpContextAccessor;

        Mellat Mellat;
        public MellatService(repository repository, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.httpContextAccessor = httpContextAccessor;

            Mellat = new Mellat
            {
                terminalId = Int64.Parse(this.repository.GetSetting("mellatTerminalId")),
                userName = this.repository.GetSetting("mellatUserName"),
                password = this.repository.GetSetting("mellatPassword")
            };
        }
        #endregion


        public string PayRequest(order order)
        {
            try
            {
                var request = httpContextAccessor.HttpContext.Request;
                UriBuilder uriBuilder = new UriBuilder();
                uriBuilder.Scheme = request.Scheme;
                uriBuilder.Host = request.Host.Host;
                uriBuilder.Path = "/Profile/Payment/MellatResponse";
                string CallBackUrl = uriBuilder.Uri.ToString();

                string res = this.Mellat.PayRequest(order.id, order.price * 10, CallBackUrl);
                return res;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void VerifyResult(order order, string SaleReferenceId)
        {
            try
            {
                repository.AddLogToOrder(order, "VerifyResult salereferenceid=" + SaleReferenceId);

                this.Mellat.VerifyResult(order.id,SaleReferenceId);


            }
            catch (Exception ex)
            {
                repository.AddLogToOrder(order, "VerifyResult result=" + ex.Message);

                throw new Exception(ex.Message);
            }
        }


        public void SettleRequest(order order)
        {
            try
            {
                this.Mellat.SettleRequest(order.id, order.bankReference);

            }
            catch (Exception ex)
            {
                repository.AddLogToOrder(order, "SettleRequest result=" + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
