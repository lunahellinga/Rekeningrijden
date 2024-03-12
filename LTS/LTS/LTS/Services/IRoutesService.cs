using LTS.DTOs;

namespace LTS.Services;

public interface IRoutesService
{
    public Task<RouteDTO> GetRoute(Guid id);

    public Task<List<RouteDTO>> GetRoutes();

    public Task PostRoute(RouteDTO route);

}