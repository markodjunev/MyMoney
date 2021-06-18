namespace MyMoney.Data.Models
{
    using System;

    using MyMoney.Data.Common.Models;
    using MyMoney.Data.Models.Enums;

    public class Deposit : BaseDeletableModel<string>
    {
        public Deposit()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public decimal EffectiveAnnualInterestRate { get; set; }

        public TypeOfCurrency Currency { get; set; }

        public int TermOfTheDeposit { get; set; }

        public string BankId { get; set; }

        public virtual Bank Bank { get; set; }

        public int TypeOfDepositId { get; set; }

        public virtual TypeOfDeposit TypeOfDeposit { get; set; }

        public int TypeOfPaymentOfInterestId { get; set; }

        public virtual TypeOfPaymentOfInterest TypeOfPaymentOfInterest { get; set; }

        public int WhoIsDepositForId { get; set; }

        public virtual WhoIsDepositFor WhoIsDepositFor { get; set; }

        public int TypeOfInterestId { get; set; }

        public virtual TypeOfInterest TypeOfInterest { get; set; }

        public int AdditionOfAmountsId { get; set; }

        public virtual AdditionOfAmounts AdditionOfAmounts { get; set; }

        public int OverdraftPossibilityId { get; set; }

        public virtual OverdraftPossibility OverdraftPossibility { get; set; }

        public int OpportunityForCreditId { get; set; }

        public virtual OpportunityForCredit OpportunityForCredit { get; set; }
    }
}
