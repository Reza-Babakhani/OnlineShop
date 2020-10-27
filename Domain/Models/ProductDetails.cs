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

        public string Title_En { get; set; }

        public string Title_Fa { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string IndexImage { get; set; }


        public DateTime UpdateDate { get; set; }


    }
}
