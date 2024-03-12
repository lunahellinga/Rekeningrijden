using Microsoft.AspNetCore.Mvc;
using RekeningRijden.RabbitMq;
using Router_Api.DTOs;
using Router_Api.Models;
using Router_Api.Services;

namespace Router_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteApiController : ControllerBase
    {
        private readonly IRouterApiService _routerApiService;

        public RouteApiController(IRouterApiService routerApiService)
        {
            _routerApiService = routerApiService;
        }


        [HttpPost("/raw")]
        public async Task SubmitRaw(RawInputDTO dto)
        {
            try
            {
                await _routerApiService.Write(dto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("/get")]
        public async Task<List<Coordinates>> GetCords()
        {
            try
            {
                return await _routerApiService.GetCoordinates();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("/status")]
        public async Task<bool> GetStatus(StatusDTO dto)
        {
            return await _routerApiService.GetStatus(dto);
        }

        [HttpGet("/getById")]
        public async Task<List<LatLongDto>> GetCordsById(string id, DateTime? start, DateTime? end)
        {
            try
            {
                return await _routerApiService.GetCordsByVehicle(id, start, end);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("/getAllVehicleIDs")]
        public async Task<List<string>> GetAllVehicleIDs()
        {
            try
            {
                return await _routerApiService.GetAllVehicleIDs();
            }
            catch (Exception ex)
            {

                throw;
            }




        }


    }
    }