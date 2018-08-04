using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities 
{
    public class postCategory:baseEntity
    {
        [Display(Name = "تصویر")]
        public string image { get; set; }
        [Display(Name = "سی اس اس")]
        public string cssClass { get; set; }
        public int? fatherId { get; set; }
        [Display(Name = "والد")]
        public postCategory father { get; set; }
        public List<postCategory> childs { get; set; }
        public List<post_postCategory> posts { get; set; }
    }
}
