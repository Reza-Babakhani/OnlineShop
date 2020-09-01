using Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels.Account
{
  public  class ForgetPasswordViewModel
    {
        [PersianRequired]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
