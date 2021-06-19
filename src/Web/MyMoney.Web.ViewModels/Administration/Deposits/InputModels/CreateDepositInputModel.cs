namespace MyMoney.Web.ViewModels.Administration.Deposits.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyMoney.Data.Models;
    using MyMoney.Data.Models.Enums;
    using MyMoney.Services.Mapping;

    public class CreateDepositInputModel : IMapTo<Deposit>
    {
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Сума")]
        [Range(5000d, 100000d, ErrorMessage = "Сумата трябва да е между {1} и {2}")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "ЕГЛ")]
        [Range(0.01d, 100d, ErrorMessage = "Стойността трябва да е между {1} и {2}")]
        public decimal EffectiveAnnualInterestRate { get; set; }

        [Required]
        [Display(Name = "Валута")]
        public TypeOfCurrency Currency { get; set; }

        [Required]
        [Range(1, 60, ErrorMessage = "Стойността трябва да е между {1} и {2} месеца.")]
        [Display(Name = "Срок в месеци")]
        public int TermOfTheDeposit { get; set; }

        [Required]
        [Display(Name = "Банка")]
        public string BankId { get; set; }

        public IEnumerable<BankDropDownViewModel> Banks { get; set; }

        [Required]
        [Display(Name = "Вид на депозита")]
        public int TypeOfDepositId { get; set; }

        public IEnumerable<TypeOfDepositDropDownViewModel> TypeOfDeposits { get; set; }

        [Required]
        [Display(Name = "Изплащане на лихви")]
        public int TypeOfPaymentOfInterestId { get; set; }

        public IEnumerable<TypeOfPaymentOfInterestDropDownViewModel> TypeOfPaymentOfInterests { get; set; }

        [Required]
        [Display(Name = "За кого е депозита")]
        public int WhoIsDepositForId { get; set; }

        public IEnumerable<WhoIsDepositForDropDownViewModel> WhoIsDepositFor { get; set; }

        [Required]
        [Display(Name = "Вид лихва")]
        public int TypeOfInterestId { get; set; }

        public IEnumerable<TypeOfInterestDropDownViewModel> TypeOfInterests { get; set; }

        [Required]
        [Display(Name = "Довнасяне на суми")]
        public int AdditionOfAmountsId { get; set; }

        public IEnumerable<AdditionOfAmountsDropDownViewModel> AdditionOfAmounts { get; set; }

        [Required]
        [Display(Name = "Възможност за овърдрафт")]
        public int OverdraftPossibilityId { get; set; }

        public IEnumerable<OverdraftPossibilityDropDownViewModel> OverdraftPossibilities { get; set; }

        [Required]
        [Display(Name = "Възможност за кредит")]
        public int OpportunityForCreditId { get; set; }

        public IEnumerable<OpportunityForCreditDropDownViewModel> OpportunityForCredit { get; set; }
    }
}
