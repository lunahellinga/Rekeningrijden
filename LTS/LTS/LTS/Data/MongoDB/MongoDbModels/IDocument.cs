using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LTS.Data.MongoDB.MongoDBModels;

public interface IDocument            
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    string Id { get; set; }
}