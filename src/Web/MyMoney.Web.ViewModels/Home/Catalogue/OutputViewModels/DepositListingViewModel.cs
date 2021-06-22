namespace MyMoney.Web.ViewModels.Home.Catalogue
{
    using MyMoney.Data.Models;
    using MyMoney.Data.Models.Enums;
    using MyMoney.Services.Mapping;

    public class DepositListingViewModel : IMapFrom<Deposit>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string BankId { get; set; }

        public string BankName { get; set; }

        public int TypeOfPaymentOfInterestId { get; set; }

        public string TypeOfPaymentOfInterestName { get; set; }

        public int TypeOfInterestId { get; set; }

        public string TypeOfInterestName { get; set; }

        public TypeOfCurrency Currency { get; set; }
    }
}
