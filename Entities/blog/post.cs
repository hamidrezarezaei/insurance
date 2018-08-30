using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace Entities
{
   public class post : baseEntity
    {
        [Display(Name = "عنوان دوم")]
        public string title2 { get; set; }
        [Display(Name = "متا دیسکریپشن")]
        public string metaDescription { get; set; }
        [Display(Name = "متا کیورد")]
        public string metaKeywords { get; set; }
        [Display(Name = "متن")]
        [DataType(DataType.MultilineText)]
        public string content { get; set; }
        [Display(Name = "خلاصه")]
        [DataType(DataType.MultilineText)]
        public string brief { get; set; }
        [NotMapped]
        [Display(Name = "تصویر")]
        public string image
        {
            get
            {
                if (this.site != null)
                {
                    var name = this.site.name;
                    var type = "post";
                    string phisicalAddress = Directory.GetCurrentDirectory() + "\\wwwroot\\" + name + "\\image\\" + type + "\\" + this.id.ToString() + ".jpg";
                    if (System.IO.File.Exists(phisicalAddress))
                    {
                        string webAddress = "/" + name + "/image/" + type + "/" + this.id.ToString() + ".jpg";
                        return webAddress;
                    }
                }
                return "";
            }
        }
        [Display(Name = "سی اس اس")]
        public string cssClass { get; set; }
        public List<post_postCategory> categories { get; set; }
    }
}
