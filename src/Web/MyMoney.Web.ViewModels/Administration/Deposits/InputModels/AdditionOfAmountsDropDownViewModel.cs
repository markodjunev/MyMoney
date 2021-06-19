namespace MyMoney.Web.ViewModels.Administration.Deposits.InputModels
{
    using MyMoney.Data.Models;
    using MyMoney.Services.Mapping;

    public class AdditionOfAmountsDropDownViewModel : IMapFrom<AdditionOfAmounts>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
