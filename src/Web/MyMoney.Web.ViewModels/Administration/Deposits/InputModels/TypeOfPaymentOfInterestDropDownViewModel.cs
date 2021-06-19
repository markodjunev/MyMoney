namespace MyMoney.Web.ViewModels.Administration.Deposits.InputModels
{
    using MyMoney.Data.Models;
    using MyMoney.Services.Mapping;

    public class TypeOfPaymentOfInterestDropDownViewModel : IMapFrom<TypeOfPaymentOfInterest>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
