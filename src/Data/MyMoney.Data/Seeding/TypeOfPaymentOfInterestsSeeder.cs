namespace MyMoney.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyMoney.Data.Models;

    public class TypeOfPaymentOfInterestsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.TypeOfPaymentOfInterests.AnyAsync())
            {
                return;
            }

            await dbContext.TypeOfPaymentOfInterests.AddAsync(new TypeOfPaymentOfInterest
            {
                Name = "Без значение",
            });

            await dbContext.TypeOfPaymentOfInterests.AddAsync(new TypeOfPaymentOfInterest
            {
                Name = "На падеж",
            });

            await dbContext.TypeOfPaymentOfInterests.AddAsync(new TypeOfPaymentOfInterest
            {
                Name = "Авансово",
            });

            await dbContext.TypeOfPaymentOfInterests.AddAsync(new TypeOfPaymentOfInterest
            {
                Name = "Ежемесечно",
            });

            await dbContext.TypeOfPaymentOfInterests.AddAsync(new TypeOfPaymentOfInterest
            {
                Name = "На край на период",
            });
        }
    }
}
