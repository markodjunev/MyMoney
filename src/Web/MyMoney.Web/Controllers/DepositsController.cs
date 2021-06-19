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

        public IActionResult Details(string id)
        {
            var deposit = this.depositsService.GetById<DepositDetailsViewModel>(id);

            if (deposit == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            return this.View(deposit);
        }
    }
}
