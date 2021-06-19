namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ITypeOfInterestsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
