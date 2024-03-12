using Microsoft.EntityFrameworkCore;
using Router_Api.Models;

namespace Router_Api.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Coordinates> Coordinates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("MigrationConnection"));
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlite("Data Source=.\\Database\\coordinates.db");
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Coordinates>(entity => {
        //        entity.HasKey(e => e.Id);
        //        entity.Property(e => e.VehicleId).HasColumnType("VARCHAR");
        //        entity.Property(e => e.Lat).HasColumnType("VARCHAR");
        //        entity.Property(e => e.Long).HasColumnType("VARCHAR");
        //        entity.Property(e => e.Time).HasColumnType("DOUBLE");
        //    });
        //    OnModelCreatingPartial(modelBuilder);
        //}
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
