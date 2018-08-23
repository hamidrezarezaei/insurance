using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class site
    {
        public int id { get; set; }
        public string host { get; set; }
        public string name { get; set; }
        public DateTime lastAccess { get; set; }
    }
}
