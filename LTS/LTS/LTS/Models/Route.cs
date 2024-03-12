using LTS.Data.MongoDB.MongoDBModels;

namespace LTS.Models;

[BsonCollection("Routes")]
public class Route : Document
{
    public decimal PriceTotal { get; set; }

    public List<Segment> Segments { get; set; } = new List<Segment>();

}