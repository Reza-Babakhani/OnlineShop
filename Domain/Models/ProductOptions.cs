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
        public int WarrantyId { get; set; }

        public Warranty Warranty { get; set; }

        public int OptionType { get; set; }//ProductOptions Enum

        public string Description { get; set; }//مقدار نمایشی

        public string Value { get; set; }//کد رنگی یا واحد اندازه گیری



        public ICollection<ProductPrices> Prices { get; set; }//جدید ترین ردیف اخرین قیمت میباشد

        public ICollection<Inventory> Inventories { get; set; }//اخرین ردیف تعداد صحیح موجود در انبار را نمایش میدهد
    }
}
