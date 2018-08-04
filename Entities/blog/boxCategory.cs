using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class boxCategory:baseEntity
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
                    var type = "boxCategory";
                    string phisicalAddress = Directory.GetCurrentDirectory() + "\\wwwroot\\" + name + "\\image\\" + type + "\\" + this.id.ToString() + ".jpg";
                    if (System.IO.File.Exists(phisicalAddress))
                    {
                        string webAddress = "/" + name + "/image/"+type+"/" + this.id.ToString() + ".jpg";
                        return webAddress;
                    }
                }
                return "";
            }
        }
        [Display(Name = "سی اس اس")]
        public string cssClass { get; set; }
        public List<box> boxes { get; set; }
        public List<boxCategory> childs { get; set; }
        [Display(Name = "والد")]
        public int? fatherId { get; set; }
        [Display(Name = "والد")]
        public boxCategory father { get; set; }
 
    }
}
