using Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels.Account
{
    public class ResetPasswordViewModel
    {

        public string UserName { get; set; }

        public string Token { get; set; }


        [PersianRequired]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmPassword), ErrorMessage ="{0} با تکرار آن یکسان نمیباشد")]
        public string Password { get; set; }

        [PersianRequired]
        [Display(Name = "تکرار رمزعبور")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
