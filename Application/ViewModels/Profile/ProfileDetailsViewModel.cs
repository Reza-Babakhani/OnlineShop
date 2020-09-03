using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.ViewModels.Profile
{
   public class ProfileDetailsViewModel
    {
        public string FullName { get; set; }

        public string PersonalCode { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public bool NewsSubscribe { get; set; }

    }
}
