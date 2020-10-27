using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
   public class ProductSpecification
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }

    }
}
