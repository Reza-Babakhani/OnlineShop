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
        public DateTime CreateDate { get; set; }

        public SubCategoryItem Category { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("ProductDetails")]
        public int ProductDetailsId { get; set; }
        public ProductDetails ProductDetails { get; set; }

        [ForeignKey("SpecialReview")]
        public int SpecialReviewId { get; set; }
        public SpecialReview SpecialReview { get; set; }

        public ICollection<ProductOptions> Options { get; set; }

        public ICollection<ProductSpecification> Specifications { get; set; }

        public ICollection<ProductImages> Images { get; set; }
    }
}
