using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ThirdParty
{
    public class Email
    {
        public string EmailFromAddress;
        public string EmailFromPassword;
        public string EmailSmtpAddress;
        public int EmailSmtpPort;
        public bool EmailEnableSsl;
        public bool Send(string to,string subject,string body)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(EmailFromAddress);
            msg.To.Add(to);
            msg.Subject = subject;
            msg.Body = body;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = EmailSmtpAddress;
            client.Port = EmailSmtpPort;
            client.EnableSsl = EmailEnableSsl;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(EmailFromAddress, EmailFromPassword);
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                msg.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                msg.Dispose();
                return false;
            }
        }
    }
}
