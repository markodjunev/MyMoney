namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;

    public class OpportunityForCreditService : IOpportunityForCreditService
    {
        private readonly IDeletableEntityRepository<OpportunityForCredit> opportunityForCreditRepository;

        public OpportunityForCreditService(IDeletableEntityRepository<OpportunityForCredit> opportunityForCreditRepository)
        {
            this.opportunityForCreditRepository = opportunityForCreditRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<OpportunityForCredit> query =
                this.opportunityForCreditRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
