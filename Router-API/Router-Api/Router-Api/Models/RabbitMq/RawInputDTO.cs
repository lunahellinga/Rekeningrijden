using Router_Api.DTOs;

namespace RekeningRijden.RabbitMq
{
    public class RawInputDTO
    {
        public string VehicleId { get; set; }

        public List<CoordinatesDTO> Coordinates { get; set; }

        public RawInputDTO()
        {
            Coordinates = new List<CoordinatesDTO>();
        }
    }
}
