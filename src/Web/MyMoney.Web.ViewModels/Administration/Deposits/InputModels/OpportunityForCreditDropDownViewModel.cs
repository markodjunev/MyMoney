namespace MyMoney.Web.ViewModels.Administration.Deposits.InputModels
{
    using MyMoney.Data.Models;
    using MyMoney.Services.Mapping;

    public class OpportunityForCreditDropDownViewModel : IMapFrom<OpportunityForCredit>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
