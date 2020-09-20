using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
   public class SubCategoryItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام(انگلیسی)")]
        public string Name { get; set; }

        [Display(Name = "عنوان نمایشی")]
        public string Title { get; set; }

        [Display(Name = "آیکون")]
        public string Icon { get; set; }

        [Display(Name = "دسته بندی")]
        public SubCategory SubCategory { get; set; }
    }
}
