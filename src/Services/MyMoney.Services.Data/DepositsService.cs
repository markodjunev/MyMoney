namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Data.Models.Enums;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;
    using MyMoney.Web.ViewModels.Deposits.OutputViewModels;

    public class DepositsService : IDepositsService
    {
        private readonly IDeletableEntityRepository<Deposit> depositsRepository;

        public DepositsService(IDeletableEntityRepository<Deposit> depositsRepository)
        {
            this.depositsRepository = depositsRepository;
        }

        public async Task<string> CreateAsync(string name, decimal amount, decimal effectiveAnnualInterestRate, TypeOfCurrency currency,
            int termOfTheDeposit, string bankId, int typeOfDepositId, int typeOfPaymentOfInterestId, int whoIsDepositForId,
            int typeOfInterestId, int additionOfAmountsId, int overdraftPossibilityId, int opportunityForCreditId)
        {
            var deposit = new Deposit
            {
                Name = name,
                Amount = amount,
                EffectiveAnnualInterestRate = effectiveAnnualInterestRate,
                Currency = currency,
                TermOfTheDeposit = termOfTheDeposit,
                BankId = bankId,
                TypeOfDepositId = typeOfDepositId,
                TypeOfPaymentOfInterestId = typeOfPaymentOfInterestId,
                WhoIsDepositForId = whoIsDepositForId,
                TypeOfInterestId = typeOfInterestId,
                AdditionOfAmountsId = additionOfAmountsId,
                OverdraftPossibilityId = overdraftPossibilityId,
                OpportunityForCreditId = opportunityForCreditId,
            };

            await this.depositsRepository.AddAsync(deposit);
            await this.depositsRepository.SaveChangesAsync();
            return deposit.Id;
        }

        public T GetById<T>(string id)
        {
            var deposit = this.depositsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return deposit;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Deposit> query =
                this.depositsRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByCurrencyAndTypeOfPaymentOfInterestId<T>(TypeOfCurrency currency, int typeOfPaymentOfInterestId)
        {
            IQueryable<Deposit> query =
                this.depositsRepository
                .All()
                .Where(x => x.Currency.Equals(currency) && x.TypeOfPaymentOfInterestId == typeOfPaymentOfInterestId)
                .OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByBankId<T>(string bankId)
        {
            IQueryable<Deposit> query =
                this.depositsRepository
                .All()
                .Where(x => x.BankId == bankId)
                .OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public bool Exist(string id)
        {
            var exist = this.depositsRepository.All().Any(x => x.Id == id);

            return exist;
        }

        public DepositCalculationViewModel GetCalculationViewModel(string id)
        {
            var deposit = this.depositsRepository.All().FirstOrDefault(x => x.Id == id);

            var calculationViewModel = new DepositCalculationViewModel
            {
                Amount = deposit.Amount,
                EffectiveAnnualInterestRate = deposit.EffectiveAnnualInterestRate,
                TermOfTheDeposit = deposit.TermOfTheDeposit,
            };

            var finalAmount = 0m; // calculation

            /*
             implementation of final amount
             */

            calculationViewModel.FinalAmount = finalAmount;

            return calculationViewModel;
        }
    }
}
