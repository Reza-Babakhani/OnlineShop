using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Attributes
{
   public class PersianRequiredAttribute:RequiredAttribute
    {
        public PersianRequiredAttribute()
        {
            ErrorMessage = "وارد کردن {0} الزامی است";
        }

        
    }
}
