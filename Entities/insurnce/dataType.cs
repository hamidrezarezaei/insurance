using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class dataType:baseEntity
    {
        public List<dataValue> dataValues { get; set; }
        [Display(Name = "والد")]
        public int? fatherId { get; set; }
        [Display(Name = "والد")]
        public dataType father { get; set; }

    }

}
