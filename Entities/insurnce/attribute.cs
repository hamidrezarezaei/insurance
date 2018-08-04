using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class attribute : baseEntity
    {
        [Display(Name = "نام")]
        public string name { get; set; }
        [Display(Name = "مقدار")]
        public string value { get; set; }
        public int categoryId { get; set; }
        [Display(Name = "دسته")]
        public category category { get; set; }
    }
    public class attribute_client : baseClient
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
