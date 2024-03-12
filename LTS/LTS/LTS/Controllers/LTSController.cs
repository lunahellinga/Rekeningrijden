using Microsoft.AspNetCore.Mvc;
using LTS.Services;
using LTS.DTOs;

namespace LTS.Controllers;

[ApiController]
[Route("Routes")]
public class LTSController : ControllerBase
{
    private readonly IRoutesService _routesService;
    public LTSController(IRoutesService service) 
    { 
        _routesService = service;      
    }

    [HttpGet("GetRoute")]
    public async Task<RouteDTO> GetRoute(Guid id)
    {
        try
        {
            return await _routesService.GetRoute(id);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [HttpGet("GetRoutes")]
    public async Task<ActionResult<List<RouteDTO>>> GetRoutes()
    {
        try
        {
            return await _routesService.GetRoutes();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("PostRoute")]
    public async Task PostRoute(RouteDTO route)
    {
        try
        {
            await _routesService.PostRoute(route);
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}