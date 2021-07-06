namespace MyMoney.Web.ViewModels.Deposits.OutputViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DepositCalculationViewModel
    {
        public decimal Amount { get; set; }

        public decimal EffectiveAnnualInterestRate { get; set; }

        public int TermOfTheDeposit { get; set; }

        public decimal FinalAmount { get; set; }
    }
}
