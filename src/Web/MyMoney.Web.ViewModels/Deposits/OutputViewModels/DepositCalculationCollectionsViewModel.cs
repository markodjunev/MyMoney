namespace MyMoney.Web.ViewModels.Deposits.OutputViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DepositCalculationCollectionsViewModel
    {
        public ICollection<decimal> MonthlyInterestTaxes { get; set; }

        public ICollection<decimal> MonthlyInterestAmount { get; set; }

        public ICollection<decimal> MonthlyStartingAmount { get; set; }

        public ICollection<decimal> MonthlyNetPaid { get; set; }
    }
}
