namespace MyMoney.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using MyMoney.Data.Models.Enums;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Web.ViewModels.Home.Catalogue;

    public class HomeController : BaseController
    {
        private readonly IDepositsService depositsService;
        private readonly IBanksService banksService;
        private readonly ITypeOfPaymentOfInterestsService typeOfPaymentOfInterestsService;

        public HomeController(
            IDepositsService depositsService,
            IBanksService banksService,
            ITypeOfPaymentOfInterestsService typeOfPaymentOfInterestsService)
        {
            this.depositsService = depositsService;
            this.banksService = banksService;
            this.typeOfPaymentOfInterestsService = typeOfPaymentOfInterestsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ViewModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Catalogue()
        {
            ShowCatalogueViewModel model = new()
            {
                Deposits = this.depositsService.GetAll<DepositListingViewModel>(),
                Banks = this.banksService.GetAll<BankListingViewModel>(),
                TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<TypeOfPaymentOfInterestDropDownViewModel>(),
            };

            return this.View(model);
        }

        public IActionResult FilterByDropdowns(int currency, int typeOfPaymentOfInterestId)
        {
            TypeOfCurrency currency1 = (TypeOfCurrency)currency;
            ShowCatalogueViewModel input = new()
            {
                Deposits = this.depositsService.GetAllByCurrencyAndTypeOfPaymentOfInterestId<DepositListingViewModel>(currency1, typeOfPaymentOfInterestId),
                Banks = this.banksService.GetAll<BankListingViewModel>(),
                TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<TypeOfPaymentOfInterestDropDownViewModel>(),
            };

            return this.View("Catalogue", input);
        }

        public IActionResult FilterByBank(string id)
        {
            ShowCatalogueViewModel input = new()
            {
                Deposits = this.depositsService.GetAllByBankId<DepositListingViewModel>(id),
                Banks = this.banksService.GetAll<BankListingViewModel>(),
                TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<TypeOfPaymentOfInterestDropDownViewModel>(),
            };

            return this.View("Catalogue", input);
        }
    }
}
