namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyMoney.Data.Models.Enums;
    using MyMoney.Web.ViewModels.Deposits.OutputViewModels;

    public interface IDepositsService
    {
        Task<string> CreateAsync(string name, decimal amount, decimal effectiveAnnualInterestRate, TypeOfCurrency currency, int termOfTheDeposit,
            string bankId, int typeOfDepositId, int typeOfPaymentOfInterestId, int whoIsDepositForId, int typeOfInterestId, int additionOfAmountsId,
            int overdraftPossibilityId, int opportunityForCreditId);

        T GetById<T>(string id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllByCurrencyAndTypeOfPaymentOfInterestId<T>(TypeOfCurrency currency, int typeOfPaymentOfInterestId);

        IEnumerable<T> GetAllByBankId<T>(string bankId);

        bool Exist(string id);

        DepositCalculationViewModel GetCalculationViewModel(string id);
    }
}
