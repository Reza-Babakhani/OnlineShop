using System;
using System.Collections.Generic;
using System.Text;
using Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [PersianRequired]
        [Display(Name = "رمزعبور فعلی")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [PersianRequired]
        [Display(Name = "رمزعبور جدید")]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmPassword), ErrorMessage = "{0} با تکرار آن یکسان نمیباشد")]
        public string Password { get; set; }

        [PersianRequired]
        [Display(Name = "تکرار رمزعبور جدید")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
