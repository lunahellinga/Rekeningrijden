using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using VehicleService.Models;

namespace VehicleService.Data;

public class Seed
{
    public static async Task SeedVehicles(DataContext dataContext)
    {
        if (await dataContext.Vehicles.AnyAsync()) return;

        var vehicles = (await File.ReadAllLinesAsync("Data/SeedData/vehicles.csv"))
            .Skip(1)
            .Select(Vehicle.FromCsv)
            .ToList();
        // TODO: Add user creation via API call to register endpoint 
        var bulkConfig = new BulkConfig { PropertiesToExclude = new List<string> { nameof(Vehicle.Id) } };
        await dataContext.BulkInsertAsync(vehicles, bulkConfig);
        await dataContext.SaveChangesAsync();
    }
}