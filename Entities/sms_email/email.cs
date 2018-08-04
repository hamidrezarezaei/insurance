using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class email:baseEntity
    {
        [Display(Name = "عنوان ایمیل")]
        public string subject { get; set; }
        [Display(Name = "بدنه ایمیل")]
        public string body { get; set; }
        [Display(Name = "آدرس ایمیل")]
        public string emailAddress { get; set; }
        public int hookId { get; set; }
        public hook hook { get; set; }
    }
}
