using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinimalApi2.Aws.Entities;
using MinimalApi2.Aws.Entities.Identity;

namespace MinimalApi2.Aws.Data
{
    public class MinimalDbContext : IdentityDbContext<User, Role, Guid>
    {
        public MinimalDbContext()
        {

        }

        public MinimalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=;port=5432;Database=MinimalDb;User Id=postgres;Password=");
            }
        }
    }
}
