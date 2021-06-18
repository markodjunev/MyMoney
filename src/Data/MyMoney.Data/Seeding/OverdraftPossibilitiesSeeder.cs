namespace MyMoney.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyMoney.Data.Models;

    public class OverdraftPossibilitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.OverdraftPossibilities.AnyAsync())
            {
                return;
            }

            await dbContext.OverdraftPossibilities.AddAsync(new OverdraftPossibility
            {
                Name = "Без значение",
            });

            await dbContext.OverdraftPossibilities.AddAsync(new OverdraftPossibility
            {
                Name = "Да",
            });

            await dbContext.OverdraftPossibilities.AddAsync(new OverdraftPossibility
            {
                Name = "Не",
            });
        }
    }
}
