using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class reminder:baseEntity
    {
        [Display(Name = "نام و نام خانوادگی")]
        public string fullName { get; set; }
        [Display(Name = "شماره موبایل")]
        public string mobile { get; set; }
        [Display(Name = "روز اتمام بیمه")]
        public int day{ get; set; }
        [Display(Name = "ماه اتمام بیمه")]
        public int month { get; set; }
        [Display(Name = "ایمیل")]
        public string email { get; set; }
        [Display(Name = "نوع بیمه نامه")]
        public string insuranceType { get; set; }
        [Display(Name = "توضیح ضمیمه")]
        public string comment { get; set; }
    }
}
