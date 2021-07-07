namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;

    public class TypeOfPaymentOfInterestsService : ITypeOfPaymentOfInterestsService
    {
        private readonly IDeletableEntityRepository<TypeOfPaymentOfInterest> typeOfPaymentOfInterestsRepository;

        public TypeOfPaymentOfInterestsService(IDeletableEntityRepository<TypeOfPaymentOfInterest> typeOfPaymentOfInterestsRepository)
        {
            this.typeOfPaymentOfInterestsRepository = typeOfPaymentOfInterestsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<TypeOfPaymentOfInterest> query =
                this.typeOfPaymentOfInterestsRepository.All().OrderBy(x => x.Id).Reverse();

            return query.To<T>().ToList();
        }
    }
}
