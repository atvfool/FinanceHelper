using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceHelper.Models;

namespace FinanceHelper.Utilities

{
    public class Amortization
    {
        public string Name { get; set; }
        public int AccountID { get; set; }
        public decimal Amount { get; set; }
        public decimal APR { get; set; }
        public decimal MinPayment { get; set; }
        public decimal Payment { get; set; }

        public Amortization(AccountsModel debt)
        {
            this.Name = debt.AccountName;
            this.AccountID = debt.AccountID;
            this.Amount = debt.StartingBalance;
            this.APR = debt.InterestRate;
            this.Payment = debt.MinimumPayment;
            this.MinPayment = debt.MinimumPayment;
        }

        public Amortization()
        {

        }

        /// <summary>
        /// returns a list of payments over the life of the "loan"
        /// </summary>
        /// <returns></returns>
        public List<AmortizationEntryModel> getAmortization()
        {
            List<AmortizationEntryModel> lst = new List<AmortizationEntryModel>();

            decimal AmountRemaining = Amount;
            AmortizationEntryModel ae;
            do
            {
                ae = new AmortizationEntryModel();
                ae.AccountName = this.Name;
                ae.AccountID = this.AccountID;
                ae.Month = lst.Count;
                ae.InterestPaid = getInterestCost(AmountRemaining);
                ae.AmountRemaining = AmountRemaining - (Payment - ae.InterestPaid);
                ae.Payment = Payment;

                lst.Add(ae);
                AmountRemaining = ae.AmountRemaining;

            } while (AmountRemaining > 0);

            return lst;
        }

        /// <summary>
        /// return interest cost for a given amount for 1 month
        /// </summary>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public decimal getInterestCost(decimal Amount)
        {
            decimal cost = 0;

            cost = Amount * APR / 12;

            return cost;
        }
    }
}
