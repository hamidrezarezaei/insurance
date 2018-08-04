using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class order : baseEntity
    {
        public order()
        {
            this.fields = new List<orderField>();
        }
        public int insuranceId { get; set; }
        public int userId { get; set; }
        [Display(Name = "تاریخ سفارش")]
        public DateTime dateTime { get; set; }
        [NotMapped]
        public string persianDate
        {
            get
            {
                return PersianDate(this.dateTime);
            }
        }
        [NotMapped]
        public string persianTime
        {
            get
            {
                return PersianTime(this.dateTime);
            }
        }
        public insurance insurance { get; set; }
        [Display(Name = "مبلغ")]
        public int price { get; set; }
        public int? paymentTypeId { get; set; }
        [Display(Name = "نوع پرداخت")]
        public paymentType paymentType { get; set; }
        public int orderStatusId { get; set; }
        [Display(Name = "وضعیت سفارش")]
        public orderStatus orderStatus { get; set; }
        public List<orderField> fields { get; set; }
        [Display(Name = "شناسه ارجاع بانک")]
        public string bankReference { get; set; }
        public string log { get; set; }
        [NotMapped]
        [Display(Name = "کدرهگیری")]
        public string trackingCode
        {
            get
            {
                string siteId = this.siteId.ToString();
                string id = this.id.ToString();
                return id + siteId;
            }
        }
    }
}
