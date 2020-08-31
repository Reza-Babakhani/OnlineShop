using Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels.Account
{
  public  class LoginViewModel
    {
        [PersianRequired]
        [Display(Name ="نام کاربری یا ایمیل")]
        public string UserNameOrEmail { get; set; }

        [PersianRequired]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RemmberMe { get; set; }


    }
}
