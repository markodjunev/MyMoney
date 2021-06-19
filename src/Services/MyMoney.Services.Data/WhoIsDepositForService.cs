namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;

    public class WhoIsDepositForService : IWhoIsDepositForService
    {
        private readonly IDeletableEntityRepository<WhoIsDepositFor> whoIsDepositForRepository;

        public WhoIsDepositForService(IDeletableEntityRepository<WhoIsDepositFor> whoIsDepositForRepository)
        {
            this.whoIsDepositForRepository = whoIsDepositForRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<WhoIsDepositFor> query =
                this.whoIsDepositForRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
