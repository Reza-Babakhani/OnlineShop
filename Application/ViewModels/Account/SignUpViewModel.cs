using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels.Account
{
   public class SignUpViewModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        [Remote("IsUserNameInUse", "Account", HttpMethod = "POST", AdditionalFields = "__RequestVerificationToken")]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "ایمیل")]
        [Remote("IsEmailInUse", "Account", HttpMethod = "POST", AdditionalFields = "__RequestVerificationToken")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "تکرار رمزعبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ComfirmPassword { get; set; }


    }
}
