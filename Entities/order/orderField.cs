using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace Entities
{
    public class orderField : baseEntity
    {
        public string type { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public int orderId { get; set; }
        public order order { get; set; }
        [NotMapped]
        [Display(Name = "آیکن")]
        public string image
        {
            get
            {
                if (this.site != null)
                {
                    var name = this.site.name;
                    var type = "orderField";
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

        public string persianDateFormat
        {
            get
            {
                return this.PersianDate(DateTime.Parse(this.value));
            }
        }
    }
}
