namespace MyMoney.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyMoney.Data.Models;

    public class AdditionOfAmountsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.AdditionOfAmounts.AnyAsync())
            {
                return;
            }

            await dbContext.AdditionOfAmounts.AddAsync(new AdditionOfAmounts
            {
                Name = "Без значение",
            });

            await dbContext.AdditionOfAmounts.AddAsync(new AdditionOfAmounts
            {
                Name = "Да",
            });

            await dbContext.AdditionOfAmounts.AddAsync(new AdditionOfAmounts
            {
                Name = "Не",
            });
        }
    }
}
