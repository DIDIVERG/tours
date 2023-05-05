using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace Second;

public class ToursContextFactory : IDesignTimeDbContextFactory<ToursContext>
{
   
    public NpgsqlConnectionStringBuilder connectionString = new NpgsqlConnectionStringBuilder()
    {

        Host = "localhost",
        Port = 5432,
        Database = "tours_db",
        Username = "postgres",
        Password = "Arista666"
    };

    // implementation of  IDesignTimeDbContextFactory
    public ToursContext CreateDbContext(string[] args)
    {
        
        var optionsBuilder = new DbContextOptionsBuilder<ToursContext>();
        optionsBuilder.UseNpgsql(connectionString.ConnectionString).UseSnakeCaseNamingConvention();
        return new ToursContext(optionsBuilder.Options);
    }
}