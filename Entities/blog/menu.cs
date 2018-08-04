using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class menu : baseEntity
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
                    var type = "menu";
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

        [Display(Name = "لینک")]
        public string link { get; set; }
        public List<menu> childs { get; set; }
        [Display(Name = "والد")]
        public int? fatherId { get; set; }
        [Display(Name = "والد")]
        public menu father { get; set; }
    }
}
