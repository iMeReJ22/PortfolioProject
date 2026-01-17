using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KanbanBackend.Infrastructure.Persistance;

public class KanbanDbContextFactory : IDesignTimeDbContextFactory<KanbanDbContext>
{
    public KanbanDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        var connectionsString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<KanbanDbContext>();

        optionsBuilder.UseSqlServer(connectionsString);

        return new KanbanDbContext(optionsBuilder.Options);
    }
}
