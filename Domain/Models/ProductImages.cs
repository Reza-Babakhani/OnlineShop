using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
  public  class ProductImages
    {

        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Display(Name = "توضیحات")]
        public string ImageAlt { get; set; }

        [Display(Name = "مسیر")]
        public string ImagePath { get; set; }

       
    }
}
