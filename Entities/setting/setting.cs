using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class setting:baseClass
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
