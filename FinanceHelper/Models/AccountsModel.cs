using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceHelper.Models
{
    public class AccountsModel : IValidatableObject
    {
        [Microsoft.AspNetCore.Mvc.HiddenInput]
        [Key]
        public int AccountID { get; set; }
        [Display(Name="Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string AccountName { get; set; }
        [Display(Name = "Balance")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "Balance is required.")]
        public decimal StartingBalance { get; set; }
        [Display(Name = "Interest")]
        [Required(ErrorMessage = "Interest is required.")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public decimal InterestRate { get; set; }
        [Display(Name = "Payment")]
        [Required(ErrorMessage = "Minimum Payment is required.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal MinimumPayment { get; set; }
        [Display(Name = "Day Of The Month")]
        [Range(1, 31, ErrorMessage="Day of the Month must be between 1 and 31")]
        public int DayOfMonth { get; set; }
        [Microsoft.AspNetCore.Mvc.HiddenInput]
        [Key]
        public string UserID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            decimal interestAccrued = Math.Round((StartingBalance * InterestRate / 12),2);
            if (MinimumPayment <= interestAccrued)
            {
                yield return new ValidationResult($"Minimum Payment must be more than the interest accrued monthly ({interestAccrued.ToString("$#,###.00")})", new[] {nameof( MinimumPayment) });
            }
        }


    }
}
