using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class user_paymentType : baseClass
    {
        public int userId { get; set; }
        public int paymentTypeId { get; set; }
        public paymentType paymentType { get; set; }
    }
}
