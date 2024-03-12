using Coordinate_Service.Models;
using Coordinate_Service.Models.MongoDb;
using System.Linq.Expressions;

namespace Coordinate_Service.Data.CoordinatesMongoDb
{
    public interface ICoordsRepository<TDocument> where TDocument : IDocument
    {
        IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        TDocument FilterByVehicleID(
        Expression<Func<TDocument, bool>> filterExpression);

        Task InsertOneAsync(TDocument document);

        Task UpdateArray(CoordinatesModel model);

        bool VehicleIdExists(string vehicleId);

    }
}
