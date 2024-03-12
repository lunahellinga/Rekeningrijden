using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "basePrice", PriceType = "base", ValueName = "basePrice", ValueDescription = 0.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "highway", PriceType = "modifier", ValueName = "primary", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "highway", PriceType = "modifier", ValueName = "secondary", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "highway", PriceType = "modifier", ValueName = "tertiary", ValueDescription = 1.3 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "highway", PriceType = "modifier", ValueName = "residential", ValueDescription = 1.4 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "boundary", PriceType = "modifier", ValueName = "suburb", ValueDescription = 1.05 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "boundary", PriceType = "modifier", ValueName = "tertiary", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "boundary", PriceType = "modifier", ValueName = "administrative", ValueDescription = 1.05 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "L", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "L1", ValueDescription = 1.15 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "L2", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "L3", ValueDescription = 1.25 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "L4", ValueDescription = 1.3 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "L5", ValueDescription = 1.35 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "L6", ValueDescription = 1.4 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "M", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "M1", ValueDescription = 1.15 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "M2", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "M3", ValueDescription = 1.25 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "N", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "N1", ValueDescription = 1.15 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "N2", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "N3", ValueDescription = 1.25 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "O", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "O1", ValueDescription = 1.15 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "O2", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "O3", ValueDescription = 1.25 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "O4", ValueDescription = 1.25 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "T", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "R", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "S", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "vehicleClassification", PriceType = "modifier", ValueName = "G", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "fuelType", PriceType = "modifier", ValueName = "Benzine", ValueDescription = 1.25 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "fuelType", PriceType = "modifier", ValueName = "Diesel", ValueDescription = 1.4 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "fuelType", PriceType = "modifier", ValueName = "LPG", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "fuelType", PriceType = "modifier", ValueName = "CNG", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "fuelType", PriceType = "modifier", ValueName = "Alcohol", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "fuelType", PriceType = "modifier", ValueName = "LNG", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "fuelType", PriceType = "modifier", ValueName = "Waterstof", ValueDescription = 1.1 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "fuelType", PriceType = "modifier", ValueName = "Elektriciteit", ValueDescription = 1.0 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "rushPrice", PriceType = "modifier", ValueName = "7", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "rushPrice", PriceType = "modifier", ValueName = "8", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "rushPrice", PriceType = "modifier", ValueName = "16", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "rushPrice", PriceType = "modifier", ValueName = "17", ValueDescription = 1.2 });
            modelBuilder.Entity<PricingModel>().HasData(new PricingModel { Id = Guid.NewGuid(), PriceTitle = "rushPrice", PriceType = "modifier", ValueName = "18", ValueDescription = 1.2 });
        }

        public DbSet<PricingModel> Pricing { get; set; }

        //public DbSet<test2> Tests2 { get; set; }
    }
}
