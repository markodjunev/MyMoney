namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;

    public class OverdraftPossibilitiesService : IOverdraftPossibilitiesService
    {
        private readonly IDeletableEntityRepository<OverdraftPossibility> overdraftPossibilitiesRepository;

        public OverdraftPossibilitiesService(IDeletableEntityRepository<OverdraftPossibility> overdraftPossibilitiesRepository)
        {
            this.overdraftPossibilitiesRepository = overdraftPossibilitiesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<OverdraftPossibility> query =
               this.overdraftPossibilitiesRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
