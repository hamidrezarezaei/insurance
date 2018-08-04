using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class adminMenu:baseEntity
    {
        public string area { get; set; }
        public string controller { get; set; }
        public bool showInMenu { get; set; }
    }
}
