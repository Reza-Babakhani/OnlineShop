using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
   public class Inventory
    {
        [Key]
        public int Id { get; set; }

        public ProductOptions Option { get; set; }

        [ForeignKey("Option")]
        public int OptionId { get; set; }

        public int LastQuantity { get; set; }//تعداد موجودی

        public int Amount { get; set; }//تعداد تراکنش جدید انبار

        public byte TypeOfTransaction  { get; set; }//افزایش یا کاهش

        public string Description { get; set; }//دلیل


        [DataType(DataType.DateTime)]
        public DateTime SubmitDate { get; set; }

    }
}
