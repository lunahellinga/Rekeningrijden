using Microsoft.EntityFrameworkCore;
using VehicleService.Data;
using VehicleService.DTOs;

namespace VehicleService.Services;

public class CarService : ICarService
{
    private readonly DataContext _dataContext;

    public CarService(DataContext context)
    {
        _dataContext = context;
    }

    public async Task<VehicleDto?> GetVehicle(Guid vehicleId)
    {
        var v = await _dataContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);
        return v is null ? null : new VehicleDto(v.Id, v.Licence, v.Classification, v.FuelType);
    }

    public async Task<List<VehicleDto>> GetNVehicles(int n)
    {
        var skip = (int)(new Random().NextDouble() * _dataContext.Vehicles.Count());
        return await _dataContext.Vehicles
            .Select(v => new VehicleDto(v.Id, v.Licence, v.Classification, v.FuelType))
            .Skip(skip)
            .Take(n)
            .ToListAsync();
    }

    public async Task<VehicleOwnerDto?> GetVehicleOwner(Guid vehicleId)
    {
        var v = await _dataContext.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicleId);
        return v is null ? null : new VehicleOwnerDto(v.Id, v.Owner);
    }

    public async Task<List<VehicleDto>> GetVehiclesWithOwner(Guid ownerId)
    {
        return await _dataContext.Vehicles
            .Where(v => v.Owner == ownerId)
            .Select(v => new VehicleDto(v.Id, v.Licence, v.Classification, v.FuelType))
            .ToListAsync();
    }

    public async Task<VehicleDto> GetRandomVehicleId()
    {
        var rand = new Random();
        var skip = (int)(rand.NextDouble() * _dataContext.Vehicles.Count());
        var vehicle = await _dataContext.Vehicles.Skip(skip).FirstAsync();
        return new VehicleDto(vehicle.Id, vehicle.Licence, vehicle.Classification, vehicle.FuelType);
    }
}