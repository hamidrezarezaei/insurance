using System.Collections.Generic;
using System.Net.Http;

namespace ThirdParty
{
    public class PayamResan
    {
        #region Constructor
        public string Username;
        public string Password;
        public string From;


        public PayamResan()
        {
        }
        #endregion

        public bool SendSmsByUrl(string to,string text)
        {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
               { "Username", this.Username },
               { "Password", this.Password },
               { "From", this.From},
               { "To", to },
               { "Text", text }
            };

            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync("http://www.payam-resan.com/APISend.aspx", content);
            return true;
        }
    }
}
