using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class category:baseEntity
    {

        [Display(Name = "نام")]
        public string name { get; set; }
        [Display(Name = "دسته")]
        public int termId { get; set; }
        [Display(Name = "دسته")]
        public term term { get; set; }
        public List<attribute> attributes{ get; set; }
    }
    public class category_client : baseClient
    {
        [Display(Name = "نام")]
        public string name { get; set; }

        public List<attribute_client> attributes { get; set; }
    }

}
