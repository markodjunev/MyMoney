namespace MyMoney.Web.ViewModels.Administration.Deposits.InputModels
{
    using MyMoney.Data.Models;
    using MyMoney.Services.Mapping;

    public class BankDropDownViewModel : IMapFrom<Bank>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
