using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
   public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name="نام(انگلیسی)")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name="عنوان نمایشی")]
        public string Title { get; set; }

        [Display(Name = "آیکون")]
        public string Icon { get; set; }
    }
}
