using Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels.Profile
{
   public class ProfileEditDetails
    {
        [Display(Name ="نام")]
        [PersianRequired]
        [PersianMaxLenght(50)]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [PersianRequired]
        [PersianMaxLenght(50)]
        public string LastName { get; set; }

        [Display(Name = "کد‌ملی")]
        [PersianRequired]
        [PersianMaxLenght(10)]
        [PersianMinLenght(10)]
        public string PersonalCode { get; set; }

        [Display(Name = "سال تولد")]
        [PersianRequired]
        public int BirthDayYear { get; set; }

        [Display(Name = "ماه تولد")]
        [PersianRequired]
        public int BirthDayMonth { get; set; }

        [Display(Name = "روز تولد")]
        [PersianRequired]
        public int BirthDayDay { get; set; }

        [Display(Name = "شماره موبایل")]
        [PersianRequired]
        [PersianMaxLenght(11)]
        [PersianMinLenght(11)]
        public string MobileNumber { get; set; }

        [Display(Name = "پست الکترونیک")]
        [PersianRequired]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "شماره کارت بانکی")]
        [PersianMaxLenght(16)]
        [PersianMinLenght(16)]
        public string CreditCardNumber { get; set; }

        [Display(Name = "اشتراک خبرنامه")]
        public bool NewsSubscribe { get; set; }
    }
}
