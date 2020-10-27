using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
  public  class Warranty
    {

        [Key]
        public int Id { get; set; }

        [Display(Name ="عنوان گارانتی")]
        public string Title { get; set; }
    }
}
