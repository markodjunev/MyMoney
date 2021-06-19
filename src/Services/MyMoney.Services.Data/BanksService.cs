namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;

    public class BanksService : IBanksService
    {
        private readonly IDeletableEntityRepository<Bank> banksRepository;

        public BanksService(IDeletableEntityRepository<Bank> banksRepository)
        {
            this.banksRepository = banksRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Bank> query =
                this.banksRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
