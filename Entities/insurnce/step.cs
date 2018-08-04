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
    public class step : baseEntity
    {
        [Display(Name = "نام")]
        public string name { get; set; }
        [Display(Name = "شماره مرحله")]
        public int number { get; set; }
        [NotMapped]
        [Display(Name = "تصویر")]
        public string image
        {
            get
            {
                if (this.site != null)
                {
                    var name = this.site.name;
                    var type = "step";
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
        [Display(Name = "بیمه")]
        public int insuranceId { get; set; }
        [Display(Name = "سی اس اس ناوبری")]
        public string navigationCssClass { get; set; }
        [Display(Name = "فیلدست")]
        public List<fieldSet> fieldSets { get; set; }
        [Display(Name = "متن دکمه مرحله بعد")]
        public string nextStepText { get; set; }
        [Display(Name = "سی اس اس دکمه مرحله بعد")]
        public string nextStepCssClass { get; set; }
        [Display(Name = "متن دکمه مرحله قبل")]
        public string previousStepText { get; set; }
        [Display(Name = "سی اس اس دکمه مرحله قبل")]
        public string previousStepCssClass { get; set; }
        [Display(Name = "سی اس اس قبل از دکمه مراحل")]
        public string beforStepButtonsCssClass { get; set; }
        [Display(Name = "سی اس اس قیمت")]
        public string priceCssClass { get; set; }


    }
    public class step_client : baseClient
    {
        public step_client()
        {
            this.isValidAllRequired = false;
        }
        public string name { get; set; }
        public string title { get; set; }
        public int number { get; set; }
        public int insuranceId { get; set; }
        public List<fieldSet_client> fieldSets { get; set; }
        public string nextStepText { get; set; }
        public string nextStepCssClass { get; set; }
        public string previousStepText { get; set; }
        public string previousStepCssClass { get; set; }
        public string beforStepButtonsCssClass { get; set; }
        public string priceCssClass { get; set; }
        public int orderIndex { get; set; }
        public bool isValidAllRequired { get; set; }
    }
    public class step_navigation
    {
        public string title { get; set; }
        public int number { get; set; }
        public string image { get; set; }
    }
}
