namespace MyMoney.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyMoney.Data.Models;

    public class TypeOfDepositsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.TypeOfDeposits.AnyAsync())
            {
                return;
            }

            await dbContext.TypeOfDeposits.AddAsync(new TypeOfDeposit
            {
                Name = "СТАНДАРТЕН СРОЧЕН ДЕПОЗИТ",
            });

            await dbContext.TypeOfDeposits.AddAsync(new TypeOfDeposit
            {
                Name = "ДЕПОЗИТ С МЕСЕЧНИ ВНОСКИ",
            });
        }
    }
}
