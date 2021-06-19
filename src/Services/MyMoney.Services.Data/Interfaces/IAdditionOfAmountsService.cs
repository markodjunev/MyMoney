namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IAdditionOfAmountsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
