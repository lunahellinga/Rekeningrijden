namespace Coordinate_Service.Data.MongoDB
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }

    }
}
