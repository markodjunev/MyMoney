namespace MyMoney.Web.ViewModels.Deposits.OutputViewModels
{
    using MyMoney.Data.Models;
    using MyMoney.Data.Models.Enums;
    using MyMoney.Services.Mapping;

    public class DepositDetailsViewModel : IMapFrom<Deposit>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public decimal EffectiveAnnualInterestRate { get; set; }

        public TypeOfCurrency Currency { get; set; }

        public int TermOfTheDeposit { get; set; }

        public string BankId { get; set; }

        public string BankName { get; set; }

        public int TypeOfDepositId { get; set; }

        public string TypeOfDepositName { get; set; }

        public int TypeOfPaymentOfInterestId { get; set; }

        public string TypeOfPaymentOfInterestName { get; set; }

        public int WhoIsDepositForId { get; set; }

        public string WhoIsDepositForName { get; set; }

        public int TypeOfInterestId { get; set; }

        public string TypeOfInterestName { get; set; }

        public int AdditionOfAmountsId { get; set; }

        public string AdditionOfAmountsName { get; set; }

        public int OverdraftPossibilityId { get; set; }

        public string OverdraftPossibilityName { get; set; }

        public int OpportunityForCreditId { get; set; }

        public string OpportunityForCreditName { get; set; }
    }
}
