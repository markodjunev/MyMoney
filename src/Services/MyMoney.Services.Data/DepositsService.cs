namespace MyMoney.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Data.Models.Enums;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;
    using MyMoney.Web.ViewModels.Deposits.OutputViewModels;
    using MyMoney.Web.ViewModels.Home.Catalogue;
    using MyMoney.Web.ViewModels.Home.Search;

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

            var startingAmount = deposit.Amount;                      // нач. сума
            var interest = deposit.EffectiveAnnualInterestRate / 100; // лихва
            decimal tax = 8 / 100m;                                        // данък

              // на падеж
            if (deposit.TypeOfPaymentOfInterestId == 4)
            {
                var interestAmount = startingAmount - ((startingAmount * interest) - ((startingAmount * interest) * tax));
                finalAmount = startingAmount + interestAmount;
            } // авансово
            else if (deposit.TypeOfPaymentOfInterestId == 3)
            {
                // avoid using the slow Math.Pow()
                var pow = ((1 + interest) * (1 + interest)) - 1;
                var interestAmount = (startingAmount * pow) - ((startingAmount * pow) * tax);
                finalAmount = startingAmount + interestAmount;
            } // ежемесечно
            else if (deposit.TypeOfPaymentOfInterestId == 2)
            {
                var currentAmount = startingAmount;
                var perMonthPercentInterest = deposit.EffectiveAnnualInterestRate / 12;

                foreach (int month in Enumerable.Range(1, deposit.TermOfTheDeposit))
                {
                    var currMonthInterest = currentAmount * perMonthPercentInterest;
                    var currentTax = currMonthInterest * tax;
                    currentAmount += currMonthInterest - currentTax;
                }

                finalAmount = currentAmount;
            } // на край на период
            else if (deposit.TypeOfPaymentOfInterestId == 1)
            {
                var totalMonths = deposit.TermOfTheDeposit;

                int yearCounter = 0;
                int monthCounter = 1;

                int leftoverMonths = 0;
                var leftoverAmount = 0m;

                var currentAmount = startingAmount;

                foreach (int month in Enumerable.Range(1, totalMonths))
                {
                    if (month % 12 == 0)
                    {
                        yearCounter++;
                    }

                    monthCounter++;
                }

                leftoverMonths = totalMonths - (yearCounter * 12);

                // e.g. 28 months or 18
                if (leftoverMonths > 0 && yearCounter > 0)
                {
                    foreach (int year in Enumerable.Range(1, yearCounter))
                    {
                        currentAmount += (currentAmount * interest) - ((currentAmount * interest) * tax);
                    }

                    var monthlyInterest = interest / 12;

                    leftoverAmount += currentAmount * (monthlyInterest * leftoverMonths);
                    leftoverAmount -= leftoverAmount * tax;

                    finalAmount += currentAmount + leftoverAmount;
                } // when its 12/24/36 etc, aka full year(s)
                else if (yearCounter > 0)
                {
                    foreach (int year in Enumerable.Range(1, yearCounter))
                    {
                        currentAmount += (currentAmount * interest) - ((currentAmount * interest) * tax);
                    }

                    finalAmount = currentAmount;
                } // then its just a few months
                else
                {
                    var monthlyInterest = interest / 12;

                    leftoverAmount += currentAmount * (monthlyInterest * leftoverMonths);
                    leftoverAmount -= leftoverAmount * tax;

                    finalAmount += leftoverAmount;
                }
            }

            calculationViewModel.FinalAmount = decimal.Round(finalAmount, 2, MidpointRounding.AwayFromZero);

            return calculationViewModel;
        }

        public IEnumerable<DepositListingViewModel> GetCatalogueViewModels(SearchViewModel input)
        {
            IQueryable<Deposit> query =
                this.depositsRepository
                .All()
                .Where(x =>
                        x.Amount <= input.Amount &&
                        x.TypeOfDepositId == input.TypeOfDepositId &&
                        x.Currency.Equals(input.Currency) &&
                        x.TermOfTheDeposit == input.TermOfTheDeposit)
                .OrderBy(x => x.Name);

            List<Deposit> deposits = query.ToList();

            // филтър 1
            if (input.TypeOfPaymentOfInterestId != 5)
            {
                List<Deposit> copy = new(deposits);

                foreach (Deposit element in copy)
                {
                    if (element.TypeOfPaymentOfInterestId != input.TypeOfPaymentOfInterestId)
                    {
                        deposits.Remove(element);
                    }
                }
            }

            // филтър 2
            if (input.WhoIsDepositForId != 4)
            {
                List<Deposit> copy = new(deposits);

                foreach (Deposit element in copy)
                {
                    if (element.WhoIsDepositForId != input.WhoIsDepositForId)
                    {
                        deposits.Remove(element);
                    }
                }
            }

            // филтър 3
            if (input.TypeOfInterestId != 3)
            {
                List<Deposit> copy = new(deposits);

                foreach (Deposit element in copy)
                {
                    if (element.TypeOfInterestId != input.TypeOfInterestId)
                    {
                        deposits.Remove(element);
                    }
                }
            }

            // филтър 4
            if (input.AdditionOfAmountsId != 3)
            {
                List<Deposit> copy = new(deposits);

                foreach (Deposit element in copy)
                {
                    if (element.AdditionOfAmountsId != input.AdditionOfAmountsId)
                    {
                        deposits.Remove(element);
                    }
                }
            }

            // филтър 5
            if (input.OverdraftPossibilityId != 3)
            {
                List<Deposit> copy = new(deposits);

                foreach (Deposit element in copy)
                {
                    if (element.OverdraftPossibilityId != input.OverdraftPossibilityId)
                    {
                        deposits.Remove(element);
                    }
                }
            }

            // филтър 6
            if (input.OpportunityForCreditId != 3)
            {
                List<Deposit> copy = new(deposits);

                foreach (Deposit element in copy)
                {
                    if (element.OpportunityForCreditId != input.OpportunityForCreditId)
                    {
                        deposits.Remove(element);
                    }
                }
            }

            IEnumerable<DepositListingViewModel> returner = new List<DepositListingViewModel>();

            foreach (Deposit deposit in deposits)
            {
                DepositListingViewModel temp = this.GetById<DepositListingViewModel>(deposit.Id);

                returner = returner.Concat(new[] { temp });
            }

            return returner;
        }
    }
}
