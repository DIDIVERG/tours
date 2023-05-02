using Microsoft.EntityFrameworkCore;
using Second.Models;

namespace Second;

public class ToursContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Tourist> Tourists { get; set; }
    public DbSet<TouristInfo> TouristInfos { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        base.OnModelCreating(modelBuilder);
        
    }
}