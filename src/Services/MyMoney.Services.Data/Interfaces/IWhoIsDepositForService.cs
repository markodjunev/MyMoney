namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IWhoIsDepositForService
    {
        IEnumerable<T> GetAll<T>();
    }
}
