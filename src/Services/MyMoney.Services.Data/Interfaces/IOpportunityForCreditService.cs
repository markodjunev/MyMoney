namespace MyMoney.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IOpportunityForCreditService
    {
        IEnumerable<T> GetAll<T>();
    }
}
