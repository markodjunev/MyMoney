namespace MyMoney.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyMoney.Data.Common.Repositories;
    using MyMoney.Data.Models;
    using MyMoney.Services.Data.Interfaces;
    using MyMoney.Services.Mapping;

    public class TypeOfInterestsService : ITypeOfInterestsService
    {
        private readonly IDeletableEntityRepository<TypeOfInterest> typeOfInterestsRepository;

        public TypeOfInterestsService(IDeletableEntityRepository<TypeOfInterest> typeOfInterestsRepository)
        {
            this.typeOfInterestsRepository = typeOfInterestsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<TypeOfInterest> query =
               this.typeOfInterestsRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
