using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class setting:baseEntity
    {
        [Display(Name = "نام خاصیت")]
        public string key { get; set; }
        [Display(Name = "مقدار")]
        public string value { get; set; }
    }
}
