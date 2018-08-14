using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace Entities
{
    public class baseEntity : baseClass
    {
        public baseEntity()
        {
            this.active = true;
        }
        [Display(Name = "عنوان")]
        public string title { get; set; }

        [Display(Name = "ترتیب نمایش")]
        public int orderIndex { get; set; }

        [Display(Name = "فعال")]
        public bool active { get; set; }

        //برای جاهایی مثل سفارش که میخواهیم بگوییم این سفارش مال فلان کاربر است
        [NotMapped]
        public user user { get; set; }
    }


    
}
