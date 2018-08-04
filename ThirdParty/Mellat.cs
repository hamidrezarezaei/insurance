using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ThirdParty
{
    public class Mellat
    {
        #region Constructor
        //private readonly IHttpContextAccessor httpContextAccessor;
        //private readonly repository repository;

        public Int64 terminalId;
        public string userName = "";
        public string password = "";


        //public Mellat(IHttpContextAccessor httpContextAccessor, repository repository)
        //{
        //    this.httpContextAccessor = httpContextAccessor;
        //    this.repository = repository;

        //    terminalId = Int64.Parse(this.repository.GetSetting("mellatTerminalId"));
        //    userName = this.repository.GetSetting("mellatUserName");
        //    password = this.repository.GetSetting("mellatPassword");
        //}

        

        #endregion

        static void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }

        public string PayRequest(int orderId,int orderPrice, string CallBackUrl)
        {
            try
            {
                BypassCertificateError();

                MellatWebService.PaymentGatewayClient bp = new MellatWebService.PaymentGatewayClient();
                var result = bp.bpPayRequestAsync(
                    this.terminalId,
                    this.userName,
                    this.password,
                    orderId,
                    //order.id,
                    orderPrice,
                    //order.price * 10,
                    DateTime.Now.ToString("yyyyMMdd"),
                    DateTime.Now.ToString("HHMMSS"),
                    "",
                    CallBackUrl,
                    0).Result;
                ;
                string[] res = result.Body.@return.Split(',');
                if (res[0] == "0")
                {
                    //refid
                    return res[1];
                }
                else
                {
                    return "-1";
                    //return "nok" + res[0];
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void VerifyResult(int orderId, string SaleReferenceId)
        {
            try
            {
                BypassCertificateError();
                MellatWebService.PaymentGatewayClient bp = new MellatWebService.PaymentGatewayClient();
                var result = bp.bpVerifyRequestAsync(
                   this.terminalId,
                    this.userName,
                    this.password,
                    orderId,
                    orderId,
                    long.Parse(SaleReferenceId)).Result;

                string VerifyResult = result.Body.@return;

                if (VerifyResult != "0")
                    throw new Exception(TranslateMessage(VerifyResult));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SettleRequest(int orderId,string orderBankReference)
        {
            try
            {
                MellatWebService.PaymentGatewayClient bp = new MellatWebService.PaymentGatewayClient();
                var result = bp.bpSettleRequestAsync(
                    this.terminalId,
                    this.userName,
                    this.password,
                      orderId,
                      orderId,
                      long.Parse(orderBankReference)).Result;

                string SettleResult = result.Body.@return;

                if (SettleResult != "0")
                    throw new Exception(TranslateMessage(SettleResult));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string TranslateMessage(string WebServiceResult)
        {
            switch (WebServiceResult)
            {
                case "11":
                    return "شماره کارت نا معتبر است.";
                case "12":
                    return "موجودی کافی نیست.";
                case "13":
                    return "رمز نادرست است.";
                case "14":
                    return "تعداد دفعات وارد کردن رمز بیش از حد مجاز است.";
                case "15":
                    return "کارت نا معتبر است.";
                case "16":
                    return "دفعات برداشت وجه بیش از حد مجاز است.";
                case "17":
                    return "انصراف از پرداخت.";
                case "18":
                    return "تاریخ انقضای کارت گذشته است.";
                case "19":
                    return "مبلغ برداشت وجه بيش از حد مجاز است.";
                case "111":
                    return "صادر كننده كارت نامعتبر است.";
                case "112":
                    return "خطاي سوييچ صادر كننده كارت.";
                case "113":
                    return "پاسخي از صادر كننده كارت دريافت نشد.";
                case "114":
                    return "دارنده كارت مجاز به انجام اين تراكنش نيست.";
                case "21":
                    return "پذيرنده نامعتبر است.";
                case "23":
                    return "خطاي امنيتي رخ داده است.";
                case "24":
                    return "اطلاعات كاربري پذيرنده نامعتبر است.";
                case "25":
                    return "مبلغ نامعتبر است.";
                case "31":
                    return "پاسخ نامعتبر است.";
                case "32":
                    return "فرمت اطلاعات وارد شده صحيح نمي باشد.";
                case "33":
                    return "حساب نامعتبر است.";
                case "34":
                    return "خطاي سيستمي.";
                case "35":
                    return "تاريخ نامعتبر است.";
                case "41":
                    return "شماره درخواست تكراري است.";
                case "42":
                    return "تراكنش Sale يافت نشد.";
                case "43":
                    return "قبلا درخواست Verify داده شده است.";
                case "44":
                    return "درخواست Verfiy يافت نشد.";
                case "45":
                    return "تراكنش Settle شده است.";
                case "46":
                    return "تراكنش Settle نشده است.";
                case "47":
                    return "تراكنش Settle يافت نشد.";
                case "48":
                    return "تراكنش Reverse شده است.";
                case "49":
                    return "تراكنش Refund يافت نشد.";
                case "412":
                    return "شناسه قبض نادرست استيافت نشد.";
                case "413":
                    return "شناسه پرداخت نادرست است.";
                case "414":
                    return "سازمان صادر كننده قبض نامعتبر است.";
                case "415":
                    return "زمان مجاز برای پرداخت به پايان رسيده است.";
                case "416":
                    return "خطا در ثبت اطلاعات.";
                case "417":
                    return "شناسه پرداخت كننده نامعتبر است.";
                case "418":
                    return "اشكال در تعريف اطلاعات مشتري.";
                case "419":
                    return "تعداد دفعات ورود اطلاعات بیشتر از حد مجاز است.";
                case "421":
                    return "Pنامعتبر است.";
                case "51":
                    return "تراكنش تكراري است.";
                case "54":
                    return "تراكنش مرجع موجود نيست.";
                case "55":
                    return "تراكنش نامعتبر است.";
                case "61":
                    return "خطا در واريز.";
                default:
                    return "خطای " + WebServiceResult + "در ارتباط با بانک.";
            }
        }

    }
}
