namespace MyMoney.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyMoney.Data.Models;

    public class BanksSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Banks.AnyAsync())
            {
                return;
            }

            await dbContext.Banks.AddAsync(new Bank
            {
                Name = "KTB",
            });

            await dbContext.Banks.AddAsync(new Bank
            {
                Name = "BNP Pirabas",
            });

            await dbContext.Banks.AddAsync(new Bank
            {
                Name = "TBIBank",
            });

            await dbContext.Banks.AddAsync(new Bank
            {
                Name = "DSK",
            });

            await dbContext.Banks.AddAsync(new Bank
            {
                Name = "Allianz Bank Bulgaria",
            });

            await dbContext.Banks.AddAsync(new Bank
            {
                Name = "Fibank",
            });
        }
    }
}
