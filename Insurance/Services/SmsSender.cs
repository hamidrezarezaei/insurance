using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using ThirdParty;

namespace Insurance.Services
{
    public class SmsSender
    {
        #region Constructor
        private readonly repository repository;

        public SmsSender(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public bool SendSms(string To, string Text)
        {
            if (this.repository.GetSetting("smsSender") == "payamResan")
            {
                PayamResan payamResan = new PayamResan
                {
                    Username = this.repository.GetSetting("payamResanUsername"),
                    Password = this.repository.GetSetting("payamResanPassword"),
                    From = this.repository.GetSetting("payamResanNumber")
                };
                payamResan.SendSmsByUrl(To, Text);

            }
            return true;
        }
    }
}
