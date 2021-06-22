namespace MyMoney.Web.ViewModels.Home.Catalogue
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyMoney.Data.Models;
    using MyMoney.Data.Models.Enums;
    using MyMoney.Services.Mapping;
    using MyMoney.Web.ViewModels.Deposits.OutputViewModels;

    public class ShowCatalogueViewModel
    {
        [Display(Name = "Банка")]
        public string BankId { get; set; }

        public IEnumerable<BankListingViewModel> Banks { get; set; }

        public int DepositId { get; set; }

        public IEnumerable<DepositListingViewModel> Deposits { get; set; }

        public int TypeOfPaymentOfInterestId { get; set; }

        public IEnumerable<TypeOfPaymentOfInterestDropDownViewModel> TypeOfPaymentOfInterests { get; set; }

        [Display(Name = "Валута")]
        public TypeOfCurrency Currency { get; set; }
    }
}
