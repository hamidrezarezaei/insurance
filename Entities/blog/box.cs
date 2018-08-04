using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class box : baseEntity
    {
        [NotMapped]
        [Display(Name = "تصویر")]
        public string image
        {
            get
            {
                if (this.site != null)
                {
                    var name = this.site.name;
                    var type = "box";
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
        [DataType(DataType.MultilineText)]
        [Display(Name = "محتوا")]
        public string content { get; set; }
        [Display(Name = "لینک")]
        public string link { get; set; }
        [Display(Name = "سی اس اس")]
        public string cssClass { get; set; }
        public int boxCategoryId { get; set; }
        public boxCategory boxCategory { get; set; }
    }
}
