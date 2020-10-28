using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
   public class SpecialReview
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Display(Name ="محتوی بررسی تخصصی")]
        public string HtmlBody { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }

    }
}
