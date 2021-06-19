namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ITypeOfPaymentOfInterestsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
