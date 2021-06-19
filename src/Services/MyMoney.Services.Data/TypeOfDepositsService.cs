namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;

    public class TypeOfDepositsService : ITypeOfDepositsService
    {
        private readonly IDeletableEntityRepository<TypeOfDeposit> typeOfDepositsRepository;

        public TypeOfDepositsService(IDeletableEntityRepository<TypeOfDeposit> typeOfDepositsRepository)
        {
            this.typeOfDepositsRepository = typeOfDepositsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<TypeOfDeposit> query =
                this.typeOfDepositsRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
