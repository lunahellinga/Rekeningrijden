using System.ComponentModel.DataAnnotations;

namespace VehicleService.Models;

public class Vehicle
{
    [Key]
    public Guid Id { get; set; }

    public string Licence { get; set; }

    public string Classification { get; set; }

    public string FuelType { get; set; }

    public Guid Owner { get; set; }

    public Vehicle(string licence, string classification, string fuelType)
    {
        Licence = licence;
        Classification = classification;
        FuelType = fuelType;
    }


    public static Vehicle FromCsv(string csvLine)
    {
        var values = csvLine.Split(',');
        return new Vehicle(values[0], values[1], values[2]);
    }
}