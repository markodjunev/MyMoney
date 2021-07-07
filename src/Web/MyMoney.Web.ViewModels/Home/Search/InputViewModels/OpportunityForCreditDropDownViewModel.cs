namespace MyMoney.Web.ViewModels.Home.Search
{
    using MyMoney.Data.Models;
    using MyMoney.Services.Mapping;

    public class OpportunityForCreditDropDownViewModel : IMapFrom<OpportunityForCredit>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
