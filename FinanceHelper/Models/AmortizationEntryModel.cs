using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceHelper.Models
{
    public class AmortizationEntryModel
    {
        public string AccountName { get; set; }
        public int AccountID { get; set; }
        [Key]
        public int Month { get; set; }
        public decimal AmountRemaining { get; set; }
        public decimal Payment { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal ExtraPayment { get; set; }
    }
}
