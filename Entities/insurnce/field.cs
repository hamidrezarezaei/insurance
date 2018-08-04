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
    public class field : baseEntity
    {


        [Display(Name = "نام")]
        public string name { get; set; }
        [Display(Name = "مقدار پیشفرض")]
        public string value { get; set; }
        [Display(Name = "والد")]
        public int? fatherid { get; set; }
        [Display(Name = "والد")]
        public field father { get; set; }
        //برای انگولار ارور دارد
        //public List<field> childs { get; set; }
        [Display(Name = "نوع")]
        public string type { get; set; }
        [Display(Name = "مقادیر")]
        public int? dataTypeid { get; set; }
        [Display(Name = "مقادیر")]
        public dataType dataType { get; set; }
        [Display(Name = "اجباری")]
        public bool isRequire { get; set; }
        [Display(Name = "عدم نمایش عنوان")]
        public bool isHideLabel { get; set; }

        [Display(Name = "سی اس اس فیلد")]
        public string fieldCssClass { get; set; }

        [Display(Name = "سی اس اس عنوان")]
        public string labelCssClass { get; set; }
        [Display(Name = "سی اس اس المان")]
        public string elementCssClass { get; set; }
        [Display(Name = "سی اس اس قبل از فیلد")]
        public string beforeCssClass { get; set; }
        [Display(Name = "سی اس اس بعد از فیلد")]
        public string afterCssClass { get; set; }
        [Display(Name = "پلیس هولدر")]
        public string placeHolder { get; set; }
        [NotMapped]
        [Display(Name = "آیکن")]
        public string image
        {
            get
            {
                if (this.site != null)
                {
                    var name = this.site.name;
                    var type = "field";
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
        [Display(Name = "فیلدست")]
        public int fieldSetId { get; set; }
        [Display(Name = "فیلدست")]
        public fieldSet fieldSet { get; set; }
        [NotMapped]
        public string titleAndType
        {
            get
            {
                return $"{this.title}  ({this.type})";
            }
        }
    }

    public class field_client : baseClient
    {
        public string name { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public int? fatherid { get; set; }
        public string type { get; set; }
        public int? dataTypeid { get; set; }

        public List<dataValue_client> dataValues { get; set; }
        //public dataType_ViewModel dataType { get; set; }
        public bool isRequire { get; set; }
        public bool isHideLabel { get; set; }
        public string fieldCssClass { get; set; }
        public string labelCssClass { get; set; }
        public string elementCssClass { get; set; }
        public string beforeCssClass { get; set; }
        public string afterCssClass { get; set; }
        public string placeHolder { get; set; }
        public string image { get; set; }
        public bool isShowLoading { get; set; }
        public int orderIndex { get; set; }

    }

}
