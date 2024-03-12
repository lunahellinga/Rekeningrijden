using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Coordinate_Service.Models.MongoDb
{
    public interface IDocument            
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        string Id { get; set; }
    }
}
