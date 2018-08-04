using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class dataValue:baseEntity
    {
        [Display(Name = "والد")]
        public int? fatherId { get; set; }
        //دیتا زیاد لود می کرد سمت کلاینت
        //public dataValue father { get; set; }
        public List<dataValue_category> categories{ get; set; }
        public int dataTypeId { get; set; }
        public dataType dataType { get; set; }
    }
    public class dataValue_client : baseClient
    {
        public string text { get; set; }
       public int? fatherid { get; set; }
        public List<dataValue_category_client> categories { get; set; }
    }
    public class dataValue_vm
    {
        public dataValue dataValue { get; set; }
        public List<term> terms { get; set; }
        public List<int> selectedCategories { get; set; }
    }
}
