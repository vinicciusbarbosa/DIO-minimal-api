using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using minimal_api.Infra.Db;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(
            "Server=localhost;Database=minimal_api;User Id=root;Password=1234;",
            new MySqlServerVersion(new Version(8, 0, 43))
        );
        
        return new AppDbContext(optionsBuilder.Options);
    }
}
