using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Attributes
{
   public class PersianMaxLenghtAttribute:MaxLengthAttribute
    {
        public PersianMaxLenghtAttribute(int lenght):base(lenght)
        {
            ErrorMessage = "{0} حداکثر {1} حرف میباشد";
        }
    }
}
