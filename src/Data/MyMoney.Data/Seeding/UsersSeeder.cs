namespace MyMoney.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyMoney.Common;
    using MyMoney.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin.a@abv.bg",
            };

            await SeedUserAsync(userManager, adminUser);

            await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var userExist = await userManager.FindByNameAsync(user.UserName);
            if (userExist == null)
            {
                var result = await userManager.CreateAsync(user, "123456");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
