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
    }

    public class term_client : baseClient
    {
        public string name { get; set; }
        public List<category> categories { get; set; }
    }
}
