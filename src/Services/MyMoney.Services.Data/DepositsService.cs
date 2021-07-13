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

        public IEnumerable<T> GetAllByCurrency<T>(TypeOfCurrency currency)
        {
            IQueryable<Deposit> query =
                this.depositsRepository
                .All()
                .Where(x => x.Currency.Equals(currency))
                .OrderBy(x => x.Name);

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

        public DepositCalculationViewModel GetCalculationViewModel(string id, decimal initialAmount)
        {
            var deposit = this.depositsRepository.All().FirstOrDefault(x => x.Id == id);

            var calculationViewModel = new DepositCalculationViewModel
            {
                Name = deposit.Name,
                Amount = deposit.Amount,
                InitialRequestedAmount = initialAmount,
                TypeOfCurrency = deposit.Currency,
                EffectiveAnnualInterestRate = deposit.EffectiveAnnualInterestRate,
                TermOfTheDeposit = deposit.TermOfTheDeposit,
                Collections = new DepositCalculationCollectionsViewModel
                {
                    MonthlyInterestTaxes = new List<decimal>(deposit.TermOfTheDeposit),
                    MonthlyInterestAmount = new List<decimal>(deposit.TermOfTheDeposit),
                    MonthlyStartingAmount = new List<decimal>(deposit.TermOfTheDeposit),
                    MonthlyNetPaid = new List<decimal>(deposit.TermOfTheDeposit),
                },
            };

            if (initialAmount != 0)
            {
                calculationViewModel.RequestedAmount = initialAmount;
            }

            var finalAmount = 0m; // calculation

            var startingAmount = calculationViewModel.Amount;                      // нач. сума
            var interest = calculationViewModel.EffectiveAnnualInterestRate / 100; // лихва
            var tax = 8 / 100m;                                                    // данък
            decimal monthlyInterest = interest / 12m;                              // данък на месец

            // на падеж
            if (deposit.TypeOfPaymentOfInterestId == 4)
            {
                var interestAmount = 0m;
                finalAmount = startingAmount;

                var interestReq = 0m;
                var finalReq = initialAmount;

                for (int i = 1; i < deposit.TermOfTheDeposit + 1; i++)
                {
                    calculationViewModel.Collections.MonthlyStartingAmount.Add(startingAmount);

                    if (i % 12 == 0 || i == deposit.TermOfTheDeposit)
                    {
                        calculationViewModel
                            .Collections
                            .MonthlyInterestAmount.Add(decimal.Round(startingAmount * interest, 2, MidpointRounding.AwayFromZero));
                        calculationViewModel
                            .Collections
                            .MonthlyInterestTaxes.Add(decimal.Round((startingAmount * interest) * tax, 2, MidpointRounding.AwayFromZero));
                        interestAmount += (startingAmount * interest) - ((startingAmount * interest) * tax);
                        interestReq += (initialAmount * interest) - ((initialAmount * interest) * tax);
                    }
                    else
                    {
                        calculationViewModel.Collections.MonthlyInterestAmount.Add(0);
                        calculationViewModel.Collections.MonthlyInterestTaxes.Add(0);
                    }

                    if (i == deposit.TermOfTheDeposit)
                    {
                        calculationViewModel.Collections.MonthlyNetPaid.Add(decimal.Round(startingAmount + interestAmount, 2, MidpointRounding.AwayFromZero));
                        finalReq += interestReq;
                    }
                    else
                    {
                        calculationViewModel.Collections.MonthlyNetPaid.Add(decimal.Round(startingAmount, 2, MidpointRounding.AwayFromZero));
                    }
                }

                calculationViewModel.RequestedAmount = decimal.Round(finalReq, 2, MidpointRounding.AwayFromZero);
                finalAmount = calculationViewModel.Collections.MonthlyNetPaid.Last();
            } // авансово
            else if (deposit.TypeOfPaymentOfInterestId == 3)
            {
                // starting amount × ((1 + interest) ^ (months÷12)−1)
                decimal pow = (decimal)(Math.Pow((double)(1m + interest), deposit.TermOfTheDeposit / 12.0) - 1.0);

                var interestAmount = (startingAmount * pow) - ((startingAmount * pow) * tax);
                finalAmount = startingAmount + interestAmount;

                var interestreq = (initialAmount * pow) - ((initialAmount * pow) * tax);
                var finalreq = initialAmount + interestreq;

                for (int i = 1; i < deposit.TermOfTheDeposit + 1; i++)
                {
                    if (i == 1)
                    {
                        calculationViewModel.Collections.MonthlyStartingAmount.Add(decimal.Round(startingAmount, 2, MidpointRounding.AwayFromZero));
                        calculationViewModel.Collections.MonthlyInterestAmount.Add(decimal.Round(startingAmount * pow, 2, MidpointRounding.AwayFromZero));
                        calculationViewModel.Collections.MonthlyInterestTaxes.Add(decimal.Round((startingAmount * pow) * tax, 2, MidpointRounding.AwayFromZero));
                        calculationViewModel.Collections.MonthlyNetPaid.Add(decimal.Round(finalAmount, 2, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        calculationViewModel.Collections.MonthlyStartingAmount.Add(decimal.Round(finalAmount, 2, MidpointRounding.AwayFromZero));
                        calculationViewModel.Collections.MonthlyInterestAmount.Add(0);
                        calculationViewModel.Collections.MonthlyInterestTaxes.Add(0);
                        calculationViewModel.Collections.MonthlyNetPaid.Add(decimal.Round(finalAmount, 2, MidpointRounding.AwayFromZero));
                    }
                }

                calculationViewModel.RequestedAmount = decimal.Round(finalreq, 2, MidpointRounding.AwayFromZero);
            } // ежемесечно
            else if (deposit.TypeOfPaymentOfInterestId == 2)
            {
                var reqAmount = calculationViewModel.RequestedAmount;
                var currentAmount = startingAmount;
                var perMonthPercentInterest = interest / 12;

                for (int i = 1; i < deposit.TermOfTheDeposit + 1; i++)
                {
                    var currMonthInterest = currentAmount * perMonthPercentInterest;
                    var currentTax = currMonthInterest * tax;

                    var currMonthInterestReq = reqAmount * perMonthPercentInterest;
                    var currentTaxReq = currMonthInterestReq * tax;

                    calculationViewModel.Collections.MonthlyInterestAmount.Add(decimal.Round(currMonthInterest, 2, MidpointRounding.AwayFromZero));
                    calculationViewModel.Collections.MonthlyInterestTaxes.Add(decimal.Round(currentTax, 2, MidpointRounding.AwayFromZero));

                    if (i == 1)
                    {
                        calculationViewModel.Collections.MonthlyStartingAmount.Add(decimal.Round(startingAmount, 2, MidpointRounding.AwayFromZero));
                        currentAmount += currMonthInterest - currentTax;
                        reqAmount += currMonthInterestReq - currentTaxReq;
                    }
                    else
                    {
                        calculationViewModel.Collections.MonthlyStartingAmount.Add(decimal.Round(currentAmount, 2, MidpointRounding.AwayFromZero));
                        currentAmount += currMonthInterest - currentTax;
                        reqAmount += currMonthInterestReq - currentTaxReq;
                    }

                    calculationViewModel.Collections.MonthlyNetPaid.Add(decimal.Round(currentAmount, 2, MidpointRounding.AwayFromZero));
                }

                finalAmount = currentAmount;
                calculationViewModel.RequestedAmount = decimal.Round(reqAmount, 2, MidpointRounding.AwayFromZero);
            } // на край на период
            else if (deposit.TypeOfPaymentOfInterestId == 1)
            {
                var totalMonths = deposit.TermOfTheDeposit;

                int yearCounter = 0;
                int monthCounter = 1;

                int leftoverMonths = 0;
                var leftoverAmount = 0m;
                var leftoverAmountReq = 0m;

                var currentAmount = startingAmount;
                var currentAmountReq = initialAmount;

                foreach (int month in Enumerable.Range(1, totalMonths))
                {
                    if (month % 12 == 0)
                    {
                        yearCounter++;
                    }

                    monthCounter++;
                }

                leftoverMonths = totalMonths - (yearCounter * 12);

                // TODO : simplify if-else, possibly in a single function
                // e.g. 28 months or 18
                if (leftoverMonths > 0 && yearCounter > 0)
                {
                    foreach (int year in Enumerable.Range(1, yearCounter))
                    {
                        currentAmount += (currentAmount * interest) - ((currentAmount * interest) * tax);
                        currentAmountReq += (currentAmountReq * interest) - ((currentAmountReq * interest) * tax);
                    }

                    leftoverAmount += currentAmount * (monthlyInterest * leftoverMonths);
                    leftoverAmount -= leftoverAmount * tax;

                    leftoverAmountReq += currentAmountReq * (monthlyInterest * leftoverMonths);
                    leftoverAmountReq -= leftoverAmountReq * tax;

                    finalAmount += currentAmount + leftoverAmount;
                    calculationViewModel.RequestedAmount = decimal.Round(currentAmountReq + leftoverAmountReq, 2, MidpointRounding.AwayFromZero);
                } // when its 12/24/36 etc, aka full year(s)
                else if (yearCounter > 0)
                {
                    foreach (int year in Enumerable.Range(1, yearCounter))
                    {
                        currentAmount += (currentAmount * interest) - ((currentAmount * interest) * tax);
                        currentAmountReq += (currentAmountReq * interest) - ((currentAmountReq * interest) * tax);
                    }

                    calculationViewModel.RequestedAmount = decimal.Round(currentAmountReq, 2, MidpointRounding.AwayFromZero);
                    finalAmount = currentAmount;
                } // then its just a few months
                else
                {
                    leftoverAmount += currentAmount * (monthlyInterest * leftoverMonths);
                    leftoverAmount -= leftoverAmount * tax;

                    leftoverAmountReq += currentAmountReq * (monthlyInterest * leftoverMonths);
                    leftoverAmountReq -= leftoverAmountReq * tax;

                    finalAmount += leftoverAmount;
                    calculationViewModel.RequestedAmount = decimal.Round(currentAmountReq + leftoverAmountReq, 2, MidpointRounding.AwayFromZero);
                }

                currentAmount = startingAmount;

                for (int i = 1; i < deposit.TermOfTheDeposit + 1; i++)
                {
                    calculationViewModel.Collections.MonthlyStartingAmount.Add(decimal.Round(startingAmount, 2, MidpointRounding.AwayFromZero));

                    if (i % 12 == 0 || i == deposit.TermOfTheDeposit)
                    {
                        calculationViewModel
                            .Collections
                            .MonthlyInterestAmount.Add(decimal.Round(startingAmount * interest, 2, MidpointRounding.AwayFromZero));
                        calculationViewModel
                            .Collections
                            .MonthlyInterestTaxes.Add(decimal.Round((startingAmount * interest) * tax, 2, MidpointRounding.AwayFromZero));
                        startingAmount += (startingAmount * interest) - ((startingAmount * interest) * tax);
                    }
                    else
                    {
                        calculationViewModel.Collections.MonthlyInterestAmount.Add(0);
                        calculationViewModel.Collections.MonthlyInterestTaxes.Add(0);
                    }

                    calculationViewModel.Collections.MonthlyNetPaid.Add(decimal.Round(startingAmount, 2, MidpointRounding.AwayFromZero));
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
