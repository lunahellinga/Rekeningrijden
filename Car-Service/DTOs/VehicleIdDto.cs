namespace VehicleService.DTOs;

public class VehicleIdDto
{
    public VehicleIdDto(Guid vehicleId)
    {
        VehicleId = vehicleId;
    }

    private Guid VehicleId { get; set; }
}