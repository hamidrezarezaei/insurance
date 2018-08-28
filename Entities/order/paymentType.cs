using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class paymentType : baseEntity
    {
        [Display(Name = "نام")]
        public string name { get; set; }
        [Display(Name = "نمایش برای همه")]
        public bool showForAll { get; set; }
        public List<user_paymentType> user_paymentType { get; set; }
    }
    public class paymentType_client : baseClient
    {
        public string title { get; set; }
    }
}

