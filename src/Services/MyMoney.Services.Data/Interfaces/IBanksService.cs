namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IBanksService
    {
        IEnumerable<T> GetAll<T>();
    }
}
