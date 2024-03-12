using LTS.Data.MongoDB.MongoDBModels;
using System.Linq.Expressions;

namespace LTS.Data.RoutesMongoDb;

public interface IRoutesRepository<TDocument> where TDocument : IDocument
{
    Task<IEnumerable<TDocument>> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression);

    IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression);

    Task<TDocument> FilterById(
        Expression<Func<TDocument, bool>> filterExpression);

    Task InsertOneAsync(TDocument document);

    //Task UpdateArray(CoordinatesModel model);

    bool VehicleIdExists(string vehicleId);

}