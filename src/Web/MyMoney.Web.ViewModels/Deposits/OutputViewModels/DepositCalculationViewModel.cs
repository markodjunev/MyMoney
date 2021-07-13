namespace MyMoney.Web.ViewModels.Deposits.OutputViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using MyMoney.Data.Models.Enums;

    public class DepositCalculationViewModel
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public TypeOfCurrency TypeOfCurrency { get; set; }

        public decimal EffectiveAnnualInterestRate { get; set; }

        public int TermOfTheDeposit { get; set; }

        public decimal FinalAmount { get; set; }

        public DepositCalculationCollectionsViewModel Collections { get; set; }

        public decimal RequestedAmount { get; set; }

        public decimal InitialRequestedAmount { get; set; }
    }
}
