using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class post_postCategory:baseClass
    {
        public post post { get; set; }

        public postCategory postCategory { get; set; }
    }
}
