using Coordinate_Service.Services;
using MassTransit;
using RekeningRijden.RabbitMq;

namespace Coordinate_Service.Consumers
{
    public class CoordsConsumer : IConsumer<RawInputDTO>
    {
        private readonly ICoordinatesServiceLayer _coordinatesServiceLayer;
        public CoordsConsumer(ICoordinatesServiceLayer coordinatesServiceLayer) 
        { 
            _coordinatesServiceLayer = coordinatesServiceLayer;
        }

        public async Task Consume(ConsumeContext<RawInputDTO> context)
        {
            RawInputDTO dto = new RawInputDTO
            {
                VehicleId = context.Message.VehicleId,
                Coordinates = context.Message.Coordinates,
            };

            await _coordinatesServiceLayer.Write(dto);
        }
    }
}
