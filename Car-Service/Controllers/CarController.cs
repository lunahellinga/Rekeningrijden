using Microsoft.AspNetCore.Mvc;
using VehicleService.DTOs;
using VehicleService.Services;

namespace VehicleService.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet("/vehicle")]
    public async Task<ActionResult<VehicleDto>> GetVehicle(Guid vehicleId)
    {
        var vehicle = await _carService.GetVehicle(vehicleId);
        return vehicle != null ? Ok(vehicle) : NotFound();
    }

    [HttpGet("/get-n")]
    public async Task<ActionResult<VehicleDto>> GetAll(int n)
    {
        var vehicles = await _carService.GetNVehicles(n);
        return vehicles.Any() ? Ok(vehicles) : NotFound();
    }

    [HttpGet("/get-owner")]
    public async Task<ActionResult<VehicleOwnerDto>> GetOwner(Guid vehicleId)
    {
        var vehicleOwner = await _carService.GetVehicleOwner(vehicleId);
        return vehicleOwner != null ? Ok(vehicleOwner) : NotFound();
    }

    [HttpGet("/owned-by")]
    public async Task<ActionResult<VehicleDto>> GetOwnedBy(Guid ownerId)
    {
        var vehicles = await _carService.GetVehiclesWithOwner(ownerId);
        return vehicles.Any() ? Ok(vehicles) : NotFound();
    }

    [HttpGet("/random")]
    public async Task<ActionResult<VehicleDto>> GetRandom()
    {
        var vehicle = await _carService.GetRandomVehicleId();
        return Ok(vehicle);
    }
}