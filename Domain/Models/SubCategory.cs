using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
   public class SubCategory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "نام(انگلیسی)")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "عنوان نمایشی")]
        public string Title { get; set; }

        [Display(Name = "آیکون")]
        public string Icon { get; set; }

        [Display(Name = "دسته بندی")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Display(Name = "دسته بندی")]
        public Category Category { get; set; }
    }
}
