using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingWebApi.Data.Models
{
    public class Expenses : BaseEntity
    {
        public string UserId { get; set; }
        public string ExpenseNumber { get; set; }

        public string ReceiptNumber { get; set; }

        public string Description { get; set; }

        public string ExpenseType { get; set; }

        public string Currency { get; set; }

        public decimal ExchangeRate { get; set; }
        public decimal ReceiptAmount { get; set; }

        /// <summary>
        /// Toplam TL
        /// </summary>
        public decimal TotalAmount { get; set; }
        public DateTime ReceiptDate { get; set; }
        public bool Approved { get; set; }
        public bool Paid { get; set; }  

    }

}
