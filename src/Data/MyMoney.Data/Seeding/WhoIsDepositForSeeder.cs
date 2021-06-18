namespace MyMoney.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyMoney.Data.Models;

    public class WhoIsDepositForSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.WhoIsDepositFor.AnyAsync())
            {
                return;
            }

            await dbContext.WhoIsDepositFor.AddAsync(new WhoIsDepositFor
            {
                Name = "Без значение",
            });

            await dbContext.WhoIsDepositFor.AddAsync(new WhoIsDepositFor
            {
                Name = "Физически лица",
            });

            await dbContext.WhoIsDepositFor.AddAsync(new WhoIsDepositFor
            {
                Name = "Пенсионери",
            });

            await dbContext.WhoIsDepositFor.AddAsync(new WhoIsDepositFor
            {
                Name = "Деца",
            });
        }
    }
}
