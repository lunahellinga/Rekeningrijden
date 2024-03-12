using Coordinate_Service.Services;
using Microsoft.AspNetCore.Mvc;
using RekeningRijden.RabbitMq;

namespace Coordinate_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoordsController : ControllerBase
    {
        private readonly ICoordinatesServiceLayer _coordinatesServiceLayer;
        public CoordsController(ICoordinatesServiceLayer serviceLayer) 
        { 
            _coordinatesServiceLayer = serviceLayer;
        
        }

        [HttpPost]
        public async Task testMethod(RawInputDTO dTO)
        {
            try
            {
                _coordinatesServiceLayer.Write(dTO);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task getCompleteModel()
        {
            try
            {
                StatusDTO dto = new StatusDTO
                {
                    VehicleID = "string1",
                    Status = 1
                };
                _coordinatesServiceLayer.Status(dto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}