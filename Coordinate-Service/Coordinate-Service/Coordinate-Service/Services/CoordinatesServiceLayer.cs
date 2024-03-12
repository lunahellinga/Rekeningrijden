using Coordinate_Service.Data.CoordinatesMongoDb;
using Coordinate_Service.DTOs;
using Coordinate_Service.Models;
using MassTransit;
using RekeningRijden.RabbitMq;

namespace Coordinate_Service.Services
{
    public class CoordinatesServiceLayer : ICoordinatesServiceLayer
    {
        private readonly ICoordsRepository<CoordinatesModel> _coordsRepository;
        private readonly IPublishEndpoint publishEndpoint;


        public CoordinatesServiceLayer(ICoordsRepository<CoordinatesModel> repository, IPublishEndpoint endpoint)         
        {
            _coordsRepository = repository;     
            publishEndpoint = endpoint;
        }

        public async Task Write(RawInputDTO dto)
        {
            CoordinatesModel document = new CoordinatesModel(dto.VehicleId);
            List<Coordinates> coordinates = new List<Coordinates>();
            
            foreach (CoordinatesDTO cord in dto.Coordinates)
            {
                coordinates.Add(new Coordinates
                {
                    Lat = cord.Lat,
                    Long = cord.Lon,
                    TimeStamp = cord.Time
                });
            }

            document.Cords = coordinates;

            try
            {
                if (_coordsRepository.VehicleIdExists(dto.VehicleId))
                {
                    await _coordsRepository.UpdateArray(document);
                }
                else
                {
                    await _coordsRepository.InsertOneAsync(document);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Status(StatusDTO dto)
        {
            if (dto.Status != 1) return;
            CoordinatesModel model = new();
            try
            {
                model = _coordsRepository.FilterByVehicleID(x => x.VehicleId == dto.VehicleID);
                if (model == null) throw new Exception("MODEL IS NULL");
            }
            catch (Exception)
            {
                throw;
            }

            PublishCoordinatesDTO response = new PublishCoordinatesDTO
            {
                VehicleId = dto.VehicleID,
                Cords = model.Cords
            };

            await publishEndpoint.Publish<PublishCoordinatesDTO>(response);
        }










    }
}
