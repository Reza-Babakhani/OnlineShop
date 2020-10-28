using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "دسته بندی")]
        public SubCategoryItem Category { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "دسته بندی")]
        public int CategoryId { get; set; }

        [ForeignKey("ProductDetails")]
        public int ProductDetailsId { get; set; }
        public ProductDetails ProductDetails { get; set; }

        

        public ICollection<ProductOptions> Options { get; set; }

        public ICollection<ProductSpecification> Specifications { get; set; }

        public ICollection<ProductImages> Images { get; set; }


        //TODO: Comments and QAs


        //کد اصلی محصول
        //ProductCode = ProductDetails.Code+"-"+ProductOption.Code
    }
}
