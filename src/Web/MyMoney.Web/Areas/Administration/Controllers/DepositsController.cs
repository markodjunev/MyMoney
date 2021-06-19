namespace MyMoney.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Web.ViewModels.Administration.Deposits.InputModels;

    public class DepositsController : AdministrationController
    {
        private readonly IDepositsService depositsService;
        private readonly IBanksService banksService;
        private readonly ITypeOfDepositsService typeOfDepositsService;
        private readonly ITypeOfPaymentOfInterestsService typeOfPaymentOfInterestsService;
        private readonly IWhoIsDepositForService whoIsDepositForService;
        private readonly ITypeOfInterestsService typeOfInterestsService;
        private readonly IAdditionOfAmountsService additionOfAmountsService;
        private readonly IOverdraftPossibilitiesService overdraftPossibilitiesService;
        private readonly IOpportunityForCreditService opportunityForCreditService;

        public DepositsController(
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

        public IActionResult Create()
        {
            var viewModel = new CreateDepositInputModel
            {
                Banks = this.banksService.GetAll<BankDropDownViewModel>(),
                TypeOfDeposits = this.typeOfDepositsService.GetAll<TypeOfDepositDropDownViewModel>(),
                TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<TypeOfPaymentOfInterestDropDownViewModel>(),
                WhoIsDepositFor = this.whoIsDepositForService.GetAll<WhoIsDepositForDropDownViewModel>(),
                TypeOfInterests = this.typeOfInterestsService.GetAll<TypeOfInterestDropDownViewModel>(),
                AdditionOfAmounts = this.additionOfAmountsService.GetAll<AdditionOfAmountsDropDownViewModel>(),
                OverdraftPossibilities = this.overdraftPossibilitiesService.GetAll<OverdraftPossibilityDropDownViewModel>(),
                OpportunityForCredit = this.opportunityForCreditService.GetAll<OpportunityForCreditDropDownViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepositInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Banks = this.banksService.GetAll<BankDropDownViewModel>();
                input.TypeOfDeposits = this.typeOfDepositsService.GetAll<TypeOfDepositDropDownViewModel>();
                input.TypeOfPaymentOfInterests = this.typeOfPaymentOfInterestsService.GetAll<TypeOfPaymentOfInterestDropDownViewModel>();
                input.WhoIsDepositFor = this.whoIsDepositForService.GetAll<WhoIsDepositForDropDownViewModel>();
                input.TypeOfInterests = this.typeOfInterestsService.GetAll<TypeOfInterestDropDownViewModel>();
                input.AdditionOfAmounts = this.additionOfAmountsService.GetAll<AdditionOfAmountsDropDownViewModel>();
                input.OverdraftPossibilities = this.overdraftPossibilitiesService.GetAll<OverdraftPossibilityDropDownViewModel>();
                input.OpportunityForCredit = this.opportunityForCreditService.GetAll<OpportunityForCreditDropDownViewModel>();

                return this.View(input);
            }

            var depositId = await this.depositsService.CreateAsync(input.Name, input.Amount, input.EffectiveAnnualInterestRate,
                input.Currency, input.TermOfTheDeposit, input.BankId, input.TypeOfDepositId, input.TypeOfPaymentOfInterestId,
                input.WhoIsDepositForId, input.TypeOfInterestId, input.AdditionOfAmountsId, input.OverdraftPossibilityId,
                input.OpportunityForCreditId);

            return this.RedirectToAction("Details", "Deposits", new { area = string.Empty, id = depositId });
        }
    }
}
