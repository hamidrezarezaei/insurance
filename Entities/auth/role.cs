using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class role:IdentityRole<int>
    {
        [NotMapped]
        public string actualName
        {
            get
            {
                try
                {
                    var arr = this.Name.Split('_');
                    string res = "";
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        res += arr[i] + "_";
                    }
                    res = res.TrimEnd('_');
                    return res;
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                this.Name = value + "_" + this.siteId;

            }
        }
        public int siteId { get; set; }
        public int updateUserId { get; set; }
        public DateTime updateDateTime { get; set; }
        public bool isDeleted { get; set; }

    }
}
