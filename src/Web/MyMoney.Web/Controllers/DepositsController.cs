namespace MyMoney.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Web.ViewModels.Deposits.OutputViewModels;

    public class DepositsController : BaseController
    {
        private readonly IDepositsService depositsService;

        public DepositsController(IDepositsService depositsService)
        {
            this.depositsService = depositsService;
        }

        public IActionResult Details(string id, bool calculate, decimal initialAmount)
        {
            if (this.depositsService.Exist(id) == false)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            _ = new DepositInfoViewModel();
            DepositInfoViewModel viewModel;

            if (!calculate)
            {
                viewModel = new DepositInfoViewModel
                {
                    DepositDetailsViewModel = this.depositsService.GetById<DepositDetailsViewModel>(id),
                };
            }
            else
            {
                viewModel = new DepositInfoViewModel
                {
                    DepositCalculationViewModel = this.depositsService.GetCalculationViewModel(id, initialAmount),
                };
            }

            return this.View(viewModel);
        }
    }
}
