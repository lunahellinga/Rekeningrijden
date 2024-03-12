namespace VehicleService.DTOs;

public class VehicleDto
{
    public VehicleDto(Guid id, string licence, string vehicleClassification, string fuelType)
    {
        Id = id;
        VehicleClassification = vehicleClassification;
        FuelType = fuelType;
        Licence = licence;
    }

    public Guid Id { get; set; }
    public string Licence { get; set; }

    public string VehicleClassification { get; set; }

    public string FuelType { get; set; }
}