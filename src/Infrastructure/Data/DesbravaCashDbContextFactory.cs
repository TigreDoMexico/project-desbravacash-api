using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data;

public class DesbravaCashDbContextFactory : IDesignTimeDbContextFactory<DesbravaCashDbContext>
{
    public DesbravaCashDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var options = new DbContextOptionsBuilder<DesbravaCashDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Postgres"))
            .Options;

        return new DesbravaCashDbContext(options);
    }
}
