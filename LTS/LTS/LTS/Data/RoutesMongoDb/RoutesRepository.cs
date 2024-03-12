using LTS.Data.MongoDB;
using LTS.Data.MongoDB.MongoDBModels;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace LTS.Data.RoutesMongoDb;

public class RoutesRepository<TDocument> : IRoutesRepository<TDocument> where TDocument : IDocument
{
    private readonly IMongoCollection<TDocument> _collection;

    public RoutesRepository(IMongoDbSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
    }

    public virtual async Task<IEnumerable<TDocument>> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression)
    {
        return await _collection.Find(filterExpression).ToListAsync();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
    {
        return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual Task InsertOneAsync(TDocument document)
    {
        return Task.Run(() => _collection.InsertOneAsync(document));
    }

    //public virtual async Task UpdateArray(CoordinatesModel model)
    //{
    //    var filter = Builders<TDocument>.Filter.Eq("VehicleId", model.VehicleId);
    //    var update = Builders<TDocument>.Update.PushEach("Cords", model.Cords);
    //    var result = await _collection.UpdateOneAsync(filter, update);
    //}

    public virtual bool VehicleIdExists(string vehicleId)
    {
        var filter = Builders<TDocument>.Filter.Eq("VehicleId", vehicleId);
        return _collection.Find(filter).Any();
    }

    public virtual async Task<TDocument> FilterById(
        Expression<Func<TDocument, bool>> filterExpression)
    {
        return await _collection.Find(filterExpression).SingleOrDefaultAsync();
    }
}