using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
  public  class ProductPrices
    {
        [Key]
        public int Id { get; set; }

        public ProductOptions Option { get; set; }

        [ForeignKey("Option")]
        public int OptionId { get; set; }

        public ulong Price { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubmitDate { get; set; }

        public string Description { get; set; }

        
        public string UserId { get; set; }



    }
}
