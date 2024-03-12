using Coordinate_Service.Services;
using MassTransit;
using RekeningRijden.RabbitMq;

namespace Coordinate_Service.Consumers
{
    public class StatusConsumer : IConsumer<StatusDTO>
    {
        private readonly ICoordinatesServiceLayer _coordinatesServiceLayer;
        public StatusConsumer(ICoordinatesServiceLayer coordinatesServiceLayer)
        {
            _coordinatesServiceLayer = coordinatesServiceLayer;
        }

        public async Task Consume(ConsumeContext<StatusDTO> context)
        {
            StatusDTO rabbit = new StatusDTO
            {
                VehicleID = context.Message.VehicleID,
                Status = context.Message.Status
            };

            await _coordinatesServiceLayer.Status(rabbit);
        }
    }
}
