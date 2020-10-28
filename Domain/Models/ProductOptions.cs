using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class ProductOptions
    {

        /// <summary>
        /// هر محصول حداقل یک ردیف از این جدول خواهد داشت که مقدار پیشفرض
        /// value
        /// برابر با 
        /// false 
        /// میشود
        /// </summary>

        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Warranty")]
        [Display(Name = "گارانتی")]
        public int WarrantyId { get; set; }

        [Display(Name = "گارانتی")]
        public Warranty Warranty { get; set; }

        [Display(Name = "نوع")]
        public int OptionType { get; set; }//ProductOptions Enum

        [Display(Name = "مقدار نمایشی")]
        public string DisplayValue { get; set; }//مقدار نمایشی

        [Display(Name = "کد رنگ یا واحد اندازه گیری")]
        public string Value { get; set; }//کد رنگی یا واحد اندازه گیری

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "کد گزینه انتخابی")]
        [Required]
        public string Code { get; set; }

        public ICollection<ProductPrices> Prices { get; set; }//جدید ترین ردیف اخرین قیمت میباشد

        public ICollection<Inventory> Inventories { get; set; }//اخرین ردیف تعداد صحیح موجود در انبار را نمایش میدهد
    }
}
