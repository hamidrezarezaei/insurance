using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class term : baseEntity
    {
        [Display(Name = "نام")]
        public string name { get; set; }
        public List<category> categories { get; set; }
        [Display(Name = "نوع داده")]
        public int? dataTypeId { get; set; }
        [Display(Name = "نوع داده")]
        public dataType dataType { get; set; }
    }

    public class term_client : baseClient
    {
        public string name { get; set; }
        public List<category> categories { get; set; }
    }
}
