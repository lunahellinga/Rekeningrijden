using Microsoft.EntityFrameworkCore;
using VehicleService.Models;

namespace VehicleService.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>()
            .Property(b => b.Id)
            .HasDefaultValueSql("uuid_generate_v4()");
    }

    public DbSet<Vehicle> Vehicles { get; set; }

}