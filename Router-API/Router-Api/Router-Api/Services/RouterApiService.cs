using MassTransit;
using Microsoft.EntityFrameworkCore;
using RekeningRijden.RabbitMq;
using Router_Api.Data;
using Router_Api.DTOs;
using Router_Api.Models;
using System.Text.Json;

namespace Router_Api.Services
{
    public class RouterApiService : IRouterApiService
    {
        private readonly DataContext _dataContext;
        private readonly IPublishEndpoint publishEndpoint;

        public RouterApiService(DataContext context, IPublishEndpoint endpoint)
        {
            _dataContext = context;
            publishEndpoint = endpoint; 
        }

        public Task<List<Coordinates>> GetCoordinates()
        {
            return _dataContext.Coordinates.ToListAsync();
        }

        public async Task Write(RawInputDTO dto)
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            //string vehicleId = dto.VehicleId;

            //foreach (CoordinatesDTO cord in dto.Coordinates)
            //{
            //    coordinates.Add(new Coordinates
            //    {
            //        VehicleId = vehicleId,
            //        Lat = cord.Lat,
            //        Long = cord.Lon,
            //        Time = cord.Time
            //    });
            //}

            try
            {
                await publishEndpoint.Publish<RawInputDTO>(dto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> GetStatus(StatusDTO dto)
        {
            await publishEndpoint.Publish<StatusDTO>(dto);

            //var jsonList = JsonSerializer.Serialize(coordinates);
            //await publishEndpoint.Publish(jsonList);

            ////verwijderen van alle coordinaten na het verzenden
            //_dataContext.RemoveRange(coordinates);
            //await _dataContext.SaveChangesAsync();
            //return statusComplete;
            return true;
        }

        public async Task<List<LatLongDto>> GetCordsByVehicle(string id, DateTime? start, DateTime? end)
        {
            List<Coordinates> cords = await _dataContext.Coordinates.Where(x => x.VehicleId == id).ToListAsync();
            cords = cords.OrderBy(x => x.Time).ToList();
            List<LatLongDto> latlong = new List<LatLongDto>();  

            foreach (Coordinates cord in cords)
            {
                latlong.Add(new LatLongDto
                {
                    Lat = cord.Lat,
                    Long = cord.Long
                });
            }
            return latlong;
        }

        public async Task<List<string>> GetAllVehicleIDs()
        {
            return _dataContext.Coordinates.Select(x => x.VehicleId).Distinct().ToList();
        }

    }
}
