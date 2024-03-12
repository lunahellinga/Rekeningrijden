using AutoMapper;
using LTS.Data.RoutesMongoDb;
using LTS.DTOs;
using LTS.Models;
using System.Text.Json;
using System.Text;
using Route = LTS.Models.Route;

namespace LTS.Services;

public class RoutesService : IRoutesService
{
    private readonly IRoutesRepository<Route> _routesRepository;
    private readonly IMapper _mapper;

    public RoutesService(IRoutesRepository<Route> routesRepository, IMapper mapper)
    {
        _routesRepository = routesRepository;
        _mapper = mapper;
    }

    public async Task<RouteDTO> GetRoute(Guid id)
    {
        var route = await _routesRepository.FilterById(x => x.Id == id.ToString());
        Console.WriteLine(route);
        return ConvertToDTO(route);
    }

    public async Task<List<RouteDTO>> GetRoutes()
    {
        List<RouteDTO> res = new();
        var routes = await _routesRepository.FilterBy(_=> true);

        Console.WriteLine(routes);

        foreach (var route in routes) 
        { 
            res.Add(ConvertToDTO(route));       
        }
        Console.WriteLine("Number of routes: " + res.Count);
        return res;
    }

    public async Task PostRoute(RouteDTO route)
    {
        try
        {
            using StringContent jsonContent = new(
            JsonSerializer.Serialize(route),
            Encoding.UTF8,
            "application/json");
            Console.WriteLine(jsonContent);
            await _routesRepository.InsertOneAsync(ConvertToModel(route));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private Route ConvertToModel(RouteDTO dto)
    {
        var route = new Route();
        var segments = new List<Segment>();


        route.Id = dto.Id.ToString();
        ;
        route.PriceTotal = dto.PriceTotal;

        foreach (var seg in dto.Segments)
        {
            var segment = new Segment();

            segment.Time = seg.Time;
            segment.Price = seg.Price;
            segment.Start = _mapper.Map<Node>(seg.Start);
            segment.Way = _mapper.Map<Way>(seg.Way);
            segment.End = _mapper.Map<Node>(seg.End);
            segments.Add(segment);
        }

        route.Segments = segments;
        return route;
    }

    private RouteDTO ConvertToDTO(Route route)
    {
        var dto = new RouteDTO();
        var segments = new List<SegmentDTO>();

        dto.Id = Guid.Parse(route.Id);
        dto.PriceTotal = route.PriceTotal;

        foreach (var seg in route.Segments)
        {
            var segment = new SegmentDTO();

            segment.Time = seg.Time;
            segment.Price = seg.Price;
            segment.Start = _mapper.Map<NodeDTO>(seg.Start);
            segment.Way = _mapper.Map<WayDTO>(seg.Way);
            segment.End = _mapper.Map<NodeDTO>(seg.End);
            segments.Add(segment);
        }

        dto.Segments = segments;
        return dto;
    }
}