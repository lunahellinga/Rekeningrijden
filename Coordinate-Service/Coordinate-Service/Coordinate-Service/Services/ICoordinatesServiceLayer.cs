using RekeningRijden.RabbitMq;

namespace Coordinate_Service.Services
{
    public interface ICoordinatesServiceLayer
    {
        Task Write(RawInputDTO dto);

        Task Status(StatusDTO dto);

    }
}
