namespace MyMoney.Web.ViewModels.Home.Catalogue
{
    using MyMoney.Data.Models;
    using MyMoney.Services.Mapping;

    public class BankListingViewModel : IMapFrom<Bank>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
