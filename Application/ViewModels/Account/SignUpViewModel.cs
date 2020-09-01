using Application.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels.Account
{
   public class SignUpViewModel
    {
        [PersianRequired]
        [Display(Name = "نام کاربری")]
        [Remote("IsUserNameInUse", "Account", HttpMethod = "POST", AdditionalFields = "__RequestVerificationToken")]
        public string UserName { get; set; }


        [PersianRequired]
        [Display(Name = "ایمیل")]
        [Remote("IsEmailInUse", "Account", HttpMethod = "POST", AdditionalFields = "__RequestVerificationToken")]
        [EmailAddress]
        public string Email { get; set; }

        [PersianRequired]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "{0} با تکرار آن یکسان نمیباشد")]
        public string Password { get; set; }

        [PersianRequired]
        [Display(Name = "تکرار رمزعبور")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}
