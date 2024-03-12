using VehicleService.DTOs;

namespace VehicleService.Services;

public interface ICarService
{
    public Task<VehicleDto?> GetVehicle(Guid vehicleId);
    public Task<List<VehicleDto>> GetNVehicles(int n);
    public Task<VehicleOwnerDto?> GetVehicleOwner(Guid vehicleId);
    public Task<List<VehicleDto>> GetVehiclesWithOwner(Guid ownerId);
    public Task<VehicleDto> GetRandomVehicleId();
}