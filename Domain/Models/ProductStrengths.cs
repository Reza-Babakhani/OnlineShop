using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
   public class ProductStrengths
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string Text { get; set; }
        //seprate with comma or enter
    }
}
