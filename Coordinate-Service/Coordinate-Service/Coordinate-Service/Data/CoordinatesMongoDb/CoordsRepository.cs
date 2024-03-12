using Coordinate_Service.Data.MongoDB;
using Coordinate_Service.Models;
using Coordinate_Service.Models.MongoDb;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Coordinate_Service.Data.CoordinatesMongoDb
{
    public class CoordsRepository<TDocument> : ICoordsRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public CoordsRepository(IMongoDbSettings settings)
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

        public virtual IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual Task InsertOneAsync(TDocument document)
        {
            document.Id = Guid.NewGuid().ToString();
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public virtual async Task UpdateArray(CoordinatesModel model)
        {
            var filter = Builders<TDocument>.Filter.Eq("VehicleId", model.VehicleId);
            var update = Builders<TDocument>.Update.PushEach("Cords", model.Cords);
            var result = await _collection.UpdateOneAsync(filter, update);
        }

        public virtual bool VehicleIdExists(string vehicleId)
        {
            var filter = Builders<TDocument>.Filter.Eq("VehicleId", vehicleId);
            return _collection.Find(filter).Any();
        }

        public virtual TDocument FilterByVehicleID(
        Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

    }
}
