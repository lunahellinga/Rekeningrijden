using RekeningRijden.RabbitMq;
using Router_Api.DTOs;
using Router_Api.Models;

namespace Router_Api.Services
{
    public interface IRouterApiService
    {
        public Task Write(RawInputDTO dto);

        public Task<List<Coordinates>> GetCoordinates();

        public Task<bool> GetStatus(StatusDTO dto);

        public Task<List<LatLongDto>> GetCordsByVehicle(string id, DateTime? start, DateTime? end);

        public Task<List<string>> GetAllVehicleIDs();

    }
}
