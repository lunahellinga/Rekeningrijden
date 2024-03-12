using Coordinate_Service.Models.MongoDb;

namespace Coordinate_Service.Models
{
    [BsonCollection("Coordinates")]
    public class CoordinatesModel : Document
    {
        public string VehicleId { get; set; } = string.Empty;

        public List<Coordinates> Cords { get; set; } = new List<Coordinates>();

        public CoordinatesModel(string id) 
        { 
            VehicleId = id; 
        }   

        public CoordinatesModel()
        {

        }
    }
}
