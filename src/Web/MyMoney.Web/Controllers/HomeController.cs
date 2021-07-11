namespace MyMoney.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using MyMoney.Data.Models.Enums;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Web.ViewModels.Home.Catalogue;
    using MyMoney.Web.ViewModels.Home.Search;

    public class HomeController : BaseController
    {
        private readonly IDepositsService depositsService;
        private readonly IBanksService banksService;
        private readonly ITypeOfPaymentOfInterestsService typeOfPaymentOfInterestsService;
        private readonly ITypeOfDepositsService typeOfDepositsService;
        private readonly IWhoIsDepositForService whoIsDepositForService;
        private readonly ITypeOfInterestsService typeOfInterestsService;
        private readonly IAdditionOfAmountsService additionOfAmountsService;
        private readonly IOverdraftPossibilitiesService overdraftPossibilitiesService;
        private readonly IOpportunityForCreditService opportunityForCreditService;

        public HomeController(
            IDepositsService depositsService,
            IBanksService banksService,
            ITypeOfDepositsService typeOfDepositsService,
            ITypeOfPaymentOfInterestsService typeOfPaymentOfInterestsService,
            IWhoIsDepositForService whoIsDepositForService,
            ITypeOfInterestsService typeOfInterestsService,
            IAdditionOfAmountsService additionOfAmountsService,
            IOverdraftPossibilitiesService overdraftPossibilitiesService,
            IOpportunityForCreditService opportunityForCreditService)
        {
            this.depositsService = depositsService;
            this.banksService = banksService;
            this.typeOfDepositsService = typeOfDepositsService;
            this.typeOfPaymentOfInterestsService = typeOfPaymentOfInterestsService;
            this.whoIsDepositForService = whoIsDepositForService;
            this.typeOfInterestsService = typeOfInterestsService;
            this.additionOfAmountsService = additionOfAmountsService;
            this.overdraftPossibilitiesService = overdraftPossibilitiesService;
            this.opportunityForCreditService = opportunityForCreditService;
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
                TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<ViewModels.Home.Catalogue.TypeOfPaymentOfInterestDropDownViewModel>(),
            };

            return this.View(model);
        }

        public IActionResult FilterByDropdowns(int currency, int typeOfPaymentOfInterestId)
        {
            TypeOfCurrency currency1 = (TypeOfCurrency)currency;

            _ = new ShowCatalogueViewModel();
            ShowCatalogueViewModel input;

            if (typeOfPaymentOfInterestId != 5)
            {
                input = new()
                {
                    Deposits = this.depositsService.GetAllByCurrencyAndTypeOfPaymentOfInterestId<DepositListingViewModel>(currency1, typeOfPaymentOfInterestId),
                    Banks = this.banksService.GetAll<BankListingViewModel>(),
                    TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<ViewModels.Home.Catalogue.TypeOfPaymentOfInterestDropDownViewModel>(),
                };
            }
            else
            {
                input = new()
                {
                    Deposits = this.depositsService.GetAllByCurrency<DepositListingViewModel>(currency1),
                    Banks = this.banksService.GetAll<BankListingViewModel>(),
                    TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<ViewModels.Home.Catalogue.TypeOfPaymentOfInterestDropDownViewModel>(),
                };
            }

            return this.View("Catalogue", input);
        }

        public IActionResult FilterByBank(string id)
        {
            ShowCatalogueViewModel input = new()
            {
                Deposits = this.depositsService.GetAllByBankId<DepositListingViewModel>(id),
                Banks = this.banksService.GetAll<BankListingViewModel>(),
                TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<ViewModels.Home.Catalogue.TypeOfPaymentOfInterestDropDownViewModel>(),
            };

            return this.View("Catalogue", input);
        }

        public IActionResult Search()
        {
            var viewModel = new SearchViewModel
            {
                TypeOfDeposits = this.typeOfDepositsService.GetAll<TypeOfDepositDropDownViewModel>(),
                TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<ViewModels.Home.Search.TypeOfPaymentOfInterestDropDownViewModel>(),
                WhoIsDepositFor = this.whoIsDepositForService.GetAll<WhoIsDepositForDropDownViewModel>(),
                TypeOfInterests = this.typeOfInterestsService.GetAll<TypeOfInterestDropDownViewModel>(),
                AdditionOfAmounts = this.additionOfAmountsService.GetAll<AdditionOfAmountsDropDownViewModel>(),
                OverdraftPossibilities = this.overdraftPossibilitiesService.GetAll<OverdraftPossibilityDropDownViewModel>(),
                OpportunityForCredit = this.opportunityForCreditService.GetAll<OpportunityForCreditDropDownViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult SearchForDeposits(SearchViewModel input)
        {
            ShowCatalogueViewModel result = new()
            {
                Deposits = this.depositsService.GetCatalogueViewModels(input),
                Banks = this.banksService.GetAll<BankListingViewModel>(),
                TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<ViewModels.Home.Catalogue.TypeOfPaymentOfInterestDropDownViewModel>(),
            };

            return this.View("Catalogue", result);
        }
    }
}
