namespace MyMoney.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyMoney.Data.Models;

    public class OpportunityForCreditSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.OpportunityForCredit.AnyAsync())
            {
                return;
            }

            await dbContext.OpportunityForCredit.AddAsync(new OpportunityForCredit
            {
                Name = "Без значение",
            });

            await dbContext.OpportunityForCredit.AddAsync(new OpportunityForCredit
            {
                Name = "Да",
            });

            await dbContext.OpportunityForCredit.AddAsync(new OpportunityForCredit
            {
                Name = "Не",
            });
        }
    }
}
