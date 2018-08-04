using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using ThirdParty;

namespace Insurance.Services
{
    public class EmailSender
    {
        #region Constructor
        private readonly repository repository;

        public EmailSender(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public bool SendEmail(string to, string subject,string body)
        {
                Email email = new Email
                {
                    EmailFromAddress = this.repository.GetSetting("EmailFromAddress"),
                    EmailFromPassword = this.repository.GetSetting("EmailFromPassword"),
                    EmailSmtpAddress = this.repository.GetSetting("EmailSmtpAddress"),
                    EmailSmtpPort = Int32.Parse(this.repository.GetSetting("EmailSmtpPort")),
                    EmailEnableSsl = bool.Parse(this.repository.GetSetting("EmailEnableSsl"))
                };
           return  email.Send(to, subject,body);
        }
    }
}
