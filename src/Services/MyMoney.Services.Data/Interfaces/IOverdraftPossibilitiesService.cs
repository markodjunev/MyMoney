namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IOverdraftPossibilitiesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
