using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class EmailSetting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("عنوان")]
        public string Title { get; set; }

        [Required]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("نام نمایشی")]
        public string DisplayName { get; set; }

        [Required]
        [DisplayName("آدرس")]
        public string Address { get; set; }

        [Required]
        [DisplayName("رمزعبور")]
        public string Password { get; set; }

        [Required]
        [DisplayName("پورت")]
        public int Port { get; set; }

        [Required]
        [DisplayName("هاست")]
        public string Host { get; set; }

        public string EncryptionKey { get; set; }


        public static EmailSetting TestEmail
        {
            get
            {

                return new EmailSetting
                { 
                    DisplayName="ایمیل تست",
                Address= "admin@reza0098.ir",
                Host= "mail.reza0098.ir",
                Password="Reza0098reza0098",
                Port=587,
                Title="ایمیل تست",
                UserName= "admin@reza0098.ir"


                };
            }
        }
    }
}
