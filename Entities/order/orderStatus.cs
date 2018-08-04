using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    //1 ناتمام
    //2 پرداخت نشده
    //3 پرداخت شده
    public enum orderStatuses
    {
        inCompleted = 1, noPayment = 2, payed = 3
    }
    public class orderStatus: baseEntity
    {
        [Display(Name = "رنگ")]
        public string color { get; set; }
    }
}
