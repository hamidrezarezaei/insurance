using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class fieldSet : baseEntity
    {
        [Display(Name = "نام")]
        public string name { get; set; }

        [Display(Name = "سی اس اس")]
        public string cssClass { get; set; }
        [Display(Name = "مرحله")]
        public int stepId { get; set; }
        [Display(Name = "مرحله")]
        public step step { get; set; }
        public List<field> fields { get; set; }
    }
    public class fieldSet_client : baseClient
    {
        public string name { get; set; }
        public string title { get; set; }
        public string cssClass { get; set; }
        public List<field_client> fields { get; set; }
        public int orderIndex { get; set; }

    }
}
