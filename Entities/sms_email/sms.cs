using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class sms:baseEntity
    {
        [Display(Name = "متن")]
        public string text { get; set; }
        [Display(Name = "شماره موبایل")]
        public string mobile { get; set; }
        public int hookId { get; set; }
        public hook hook { get; set; }
    }
}
