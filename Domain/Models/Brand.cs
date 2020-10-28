using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
   public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "عنوان برند")]
        [Required]
        public string Title { get; set; }
    }
}
