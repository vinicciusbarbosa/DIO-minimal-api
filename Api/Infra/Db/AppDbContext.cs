using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.Entities;

namespace minimal_api.Infra.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; } = default!;
        public DbSet<Vehicle> Vehicles { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .Property(a => a.Profile)
                .HasConversion<string>();

            modelBuilder.Entity<Administrator>().HasData(
                new Administrator
                {
                    Id = -1,
                    Email = "adm@teste.com",
                    Password = "123",
                    Profile = Domain.Enums.Profile.Administrator
                }
            );
        }
        
         public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }
    }
}
