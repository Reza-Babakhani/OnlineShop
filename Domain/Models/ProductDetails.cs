using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
   public class ProductDetails
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [Required]
        public string Title_En { get; set; }

        [Required]
        [Display(Name = "عنوان فارسی")]
        public string Title_Fa { get; set; }

        [Display(Name = "توضیحات - بررسی اجمالی")]
        public string Description { get; set; }

        [Display(Name = "کد محصول")]
        [Required]
        public string Code { get; set; }

        [Display(Name = "تصویر اصلی")]
        [Required]
        public string IndexImage { get; set; }

        [Display(Name = "مدت زمان آماده سازی محصول برای ارسال (روز)")]
        public int PreparationTime{ get; set; }

        [ForeignKey("Brand")]
        [Display(Name ="برند")]
        public int BrandId { get; set; }

        [Display(Name = "برند")]
        public Brand Brand { get; set; }

        public DateTime UpdateDate { get; set; }


    }
}
