using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class hook:baseEntity
    {
        [Display(Name = "نام")]
        public string name { get; set; }
        public List<sms> smses{ get; set; }
        public List<email> emails{ get; set; }
    }
}
