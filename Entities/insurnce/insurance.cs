using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class insurance:baseEntity
    {
        [Display(Name = "نام")]
        public string name { get; set; }

        [NotMapped]
        [Display(Name = "تصویر")]
        public string image
        {
            get
            {
                if (this.site != null)
                {
                    var name = this.site.name;
                    var type = "insurance";
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
        [Display(Name = "فرمول")]
        public string formula { get; set; }
        [Display(Name = "سی اس اس")]
        public string cssClass { get; set; }
        [Display(Name = "مراحل")]
         public List<step> steps { get; set; }
        [Display(Name = "تعداد مراحل")]
         public int stepCount { get; set; }
        [Display(Name = "موقعیت تب ها")]
        public string tabLocation { get; set; }
        public List<order> orders { get; set; }
    }

    public class insurance_client : baseClient
    {
        public insurance_client()
        {
            this.price = 0;
            this.currentStep = 1;
        }
        public string name { get; set; }
        public string title { get; set; }
        public string formula { get; set; }
        public string cssClass { get; set; }
        public int price { get; set; }
        public List<step_client> steps { get; set; }
        public int currentStep { get; set; }
        public int stepCount { get; set; }
        public string image { get; set; }
        public List<step_navigation> step_navigations { get; set; }
        public string tabLocation { get; set; }

    }
}
