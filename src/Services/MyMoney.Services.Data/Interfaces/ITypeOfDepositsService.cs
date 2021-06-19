namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ITypeOfDepositsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
