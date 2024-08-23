using MinimalApi2.Aws.Entities.Identity;
using MinimalApi2.Aws.Models;
using System.Reflection.Metadata;

namespace MinimalApi2.Aws.Data
{
    public class MinimalSeedContext
    {
        public async static Task SeedAsync(MinimalDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Role>()
                {
                    new Role(){Name = Constants.Role.User,NormalizedName = Constants.Role.User.ToUpper()},
                    new Role(){Name = Constants.Role.Admin,NormalizedName = Constants.Role.Admin.ToUpper()}
                };
                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }
        }
    }
}
