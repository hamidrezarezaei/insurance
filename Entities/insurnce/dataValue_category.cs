using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class dataValue_category : baseClass
    {
        public int dataValueId { get; set; }
        public dataValue dataValue { get; set; }
        public int categoryId { get; set; }
        public category category { get; set; }
    }
    public class dataValue_category_client : baseClient
    {
        //public dataValue dataValue { get; set; }
        public category_client category { get; set; }
    }

}
