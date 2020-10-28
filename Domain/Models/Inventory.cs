using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
   public class Inventory
    {
        [Key]
        public int Id { get; set; }

        public ProductOptions Option { get; set; }

        [ForeignKey("Option")]
        public int OptionId { get; set; }

        [Display(Name = "موجودی فعلی")]
        [Required]
        public int LastQuantity { get; set; }//تعداد موجودی

        [Display(Name = "تعداد تراکنش جدید انبار")]
        [Required]
        public int Amount { get; set; }//تعداد تراکنش جدید انبار

        [Display(Name = "نوع تراکنش(افزایش/کاهش)")]
        [Required]
        public byte TypeOfTransaction  { get; set; }//افزایش یا کاهش

        [Display(Name = "توضیحات")]
        public string Description { get; set; }//دلیل

        [Display(Name = "تاریخ ثبت")]
        [DataType(DataType.DateTime)]
        public DateTime SubmitDate { get; set; }

    }
}
