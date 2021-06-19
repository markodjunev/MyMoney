namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;

    public class AdditionOfAmountsService : IAdditionOfAmountsService
    {
        private readonly IDeletableEntityRepository<AdditionOfAmounts> additionOfAmountsRepository;

        public AdditionOfAmountsService(IDeletableEntityRepository<AdditionOfAmounts> additionOfAmountsRepository)
        {
            this.additionOfAmountsRepository = additionOfAmountsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<AdditionOfAmounts> query =
                this.additionOfAmountsRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
