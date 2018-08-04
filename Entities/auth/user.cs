using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class user : IdentityUser<int>
    {
        [NotMapped]
        public string actualUserName
        {
            get
            {
                var arr = this.UserName.Split('_');
                string res = "";
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    res += arr[i] + "_";
                }
                res = res.TrimEnd('_');
                return res;
            }
            set
            {
                this.UserName = value + "_" + this.siteId;

            }
        }
        public int siteId { get; set; }
        [Display(Name = "نام")]
        public string firstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string lastName { get; set; }
        [Display(Name = "کد ملی")]
        public string nationalCode { get; set; }
        [Display(Name = "آخرین تغییر دهنده")]
        public int updateUserId { get; set; }
        [Display(Name = "زمان آخرین تغییر")]
        public DateTime updateDateTime { get; set; }
        public bool isDeleted { get; set; }
        [NotMapped]
        [Display(Name = "نام کاربر")]
        public string fullName
        {
            get
            {
                return this.firstName + " " + this.lastName;
            }
        }
        //for view
        [NotMapped]
        public string role { get; set; }
        [NotMapped]
        public string actualRole
        {
            get
            {
                try
                {
                    var arr = this.role.Split('_');
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
                this.role = value + "_" + this.siteId;

            }
        }
    }
    public class user_express
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "نام کاربری")]
        public string actualUserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        //[Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }
        [Display(Name = "نام")]
        public string firstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string lastName { get; set; }
        [Display(Name = "نام کاربر")]
        public string fullName
        {
            get
            {
                return this.firstName + " " + this.lastName;
            }
        }
        [Display(Name = "ایمیل")]
        public string email { get; set; }
        [Display(Name = "موبایل")]
        public string phoneNumber { get; set; }
        [Display(Name = "کد ملی")]
        public string nationalCode { get; set; }
        public bool clientIsValid { get; set; }
        public string clientMessage { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool rememberMe { get; set; }
    }
    public class user_vm
    {
        public user_express user { get; set; }
        public List<paymentType> paymentTypes { get; set; }
        public List<int> selectedPaymentTypes { get; set; }

        public SelectList roles { get; set; }
        public string selectedRole { get; set; }
    }
}
