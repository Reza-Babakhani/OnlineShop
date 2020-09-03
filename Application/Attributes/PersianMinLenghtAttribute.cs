using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Attributes
{
  public  class PersianMinLenghtAttribute:MinLengthAttribute
    {
        public PersianMinLenghtAttribute(int lenght):base(lenght)
        {
            ErrorMessage="{0} حداقل {1} حرف میباشد";
        }
    }
}
