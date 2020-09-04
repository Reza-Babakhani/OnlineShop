using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Identity
{
   public class ApplicationUser:IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public bool NewsSubscribe { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        public string PersonalCode { get; set; }

        [MaxLength(16)]
        [MinLength(16)]
        public string CreditCardNumber { get; set; }

        public string ImagePath { get; set; }

        public DateTime SendPhoneConfirmationSmsLimitUntil { get; set; }

    }
}
