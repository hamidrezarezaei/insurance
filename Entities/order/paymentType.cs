using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class paymentType : baseEntity
    {
        public string name { get; set; }
        public bool showForAll { get; set; }
        public List<user_paymentType> user_paymentType { get; set; }
    }
    public class paymentType_client : baseClient
    {
        public string title { get; set; }
    }
}

