using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace Second;

public class ToursContextFactory : IDesignTimeDbContextFactory<ToursContext>
{
    private string connectionString;
    public string ConnectionString { get; set; }

    // implementation of  IDesignTimeDbContextFactory
    public ToursContext CreateDbContext(string[] args)
    {
        NpgsqlConnectionStringBuilder connectionString = new NpgsqlConnectionStringBuilder()
        {

            Host = "localhost",
            Port = 5432,
            Database = "tours_db",
            Username = "postgres",
            Password = "Arista666"
        };

        var optionsBuilder = new DbContextOptionsBuilder<ToursContext>();
        optionsBuilder.UseNpgsql(connectionString.ConnectionString).UseSnakeCaseNamingConvention();
        return new ToursContext(optionsBuilder.Options);
    }
}