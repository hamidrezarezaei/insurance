using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class HookManager
    {
        #region Constructor
        private readonly repository repository;
        private readonly SmsSender smsSender;
        private readonly EmailSender emailSender;

        public HookManager(repository repository, SmsSender smsSender, EmailSender emailSender)
        {
            this.repository = repository;
            this.smsSender = smsSender;
            this.emailSender = emailSender;
        }
        #endregion

        private string replacePatterns(string input, user user, order order = null)
        {
            string output = input;
            Regex r = new Regex(@"\[(.+?)\]", RegexOptions.IgnoreCase);
            Match m = r.Match(input);
            while (m.Success)
            {
                try
                {
                    string pat = m.Groups[1].Value;
                    string newVal = $"[{pat}]";
                    var arr = pat.Split('.');
                    if (arr[0] == "user")
                    {
                        newVal = user.GetType().GetProperty(arr[1]).GetValue(user, null).ToString();
                    }
                    else if (arr[0] == "order")
                    {
                        newVal = order.GetType().GetProperty(arr[1]).GetValue(order, null).ToString();
                    }
                    output = output.Replace($"[{pat}]", newVal);
                }
                catch { }
                m = m.NextMatch();
            }
            return output;
        }
        private string replacePatterns(string input, reminder reminder)
        {
            string output = input;
            Regex r = new Regex(@"\[(.+?)\]", RegexOptions.IgnoreCase);
            Match m = r.Match(input);
            while (m.Success)
            {
                try
                {
                    string pat = m.Groups[1].Value;
                    string newVal = $"[{pat}]";
                    var arr = pat.Split('.');
                    if (arr[0] == "reminder")
                    {
                        newVal = reminder.GetType().GetProperty(arr[1]).GetValue(reminder, null).ToString();
                    }
                    output = output.Replace($"[{pat}]", newVal);
                }
                catch { }
                m = m.NextMatch();
            }
            return output;
        }
        public async void HookFired(string hookName, user user, order order = null)
        {
            var smses = this.repository.GetActiveSmses(hookName);
            foreach (var sms in smses)
            {
                sms.text = this.replacePatterns(sms.text, user, order);

                if (sms.mobile != null && sms.mobile.Trim() != "")
                {
                    this.smsSender.SendSms(sms.mobile, sms.text);
                }
                else
                {
                    this.smsSender.SendSms(user.PhoneNumber, sms.text);
                }
            }

            var emails = this.repository.GetActiveEmails(hookName);
            foreach (var email in emails)
            {
                email.subject = this.replacePatterns(email.subject, user, order);
                email.body = this.replacePatterns(email.body, user, order);

                if (email.emailAddress != null && email.emailAddress.Trim() != "")
                {
                    this.emailSender.SendEmail(email.emailAddress, email.subject, email.body);
                }
                else if (user.Email != null && user.Email.Trim() != "")
                {
                    this.emailSender.SendEmail(user.Email, email.subject, email.body);
                }
            }
        }

        public async void HookFired(string hookName, List<reminder> reminders)
        {
            var smses = this.repository.GetActiveSmses(hookName);
            foreach (var reminder in reminders)
            {
                foreach (var sms in smses)
                {
                    sms.text = this.replacePatterns(sms.text, reminder);

                    if (sms.mobile != null && sms.mobile.Trim() != "")
                    {
                        this.smsSender.SendSms(sms.mobile, sms.text);
                    }
                    else
                    {
                        this.smsSender.SendSms(reminder.mobile, sms.text);
                    }
                }
            }

            var emails = this.repository.GetActiveEmails(hookName);
            foreach (var reminder in reminders)
            {
                foreach (var email in emails)
                {
                    email.subject = this.replacePatterns(email.subject, reminder);
                    email.body = this.replacePatterns(email.body, reminder);

                    if (email.emailAddress != null && email.emailAddress.Trim() != "")
                    {
                        this.emailSender.SendEmail(email.emailAddress, email.subject, email.body);
                    }
                    else if (reminder.email != null && reminder.email.Trim() != "")
                    {
                        this.emailSender.SendEmail(reminder.email, email.subject, email.body);
                    }
                }
            }
        }
    }
}
