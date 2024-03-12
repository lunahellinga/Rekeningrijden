namespace VehicleService.DTOs;

public class VehicleOwnerDto
{
    public VehicleOwnerDto(Guid vehicleId, Guid ownerId)
    {
        VehicleId = vehicleId;
        OwnerId = ownerId;
    }

    public Guid VehicleId { get; set; }
    public Guid OwnerId { get; set; }
}