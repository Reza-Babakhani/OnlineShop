using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace Domain.Models
{
   public class SmsSetting
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("عنوان")]
        public string Title { get; set; }

        [DisplayName("کلید وب سرویس (Api Key)")]
        public string ApiKey { get; set; }

        [DisplayName("کد امنیتی (SecurityCode)")]
        public string SecurityCode { get; set; }

        public static SmsSetting testSms
        {
            get
            {
                return new SmsSetting { Title = "تست اس ام اس", ApiKey = "b6ee57b32fc5f5aa8d5931ec", SecurityCode = "RezaBaBaSms" };
            }
        }
    }
}
