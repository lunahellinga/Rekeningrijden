using AutoMapper;
using LTS.DTOs;
using LTS.Services;
using MassTransit;

namespace LTS.Consumer
{
    public class RouteConsumer : IConsumer<RouteDTO>
    {
        private readonly IRoutesService _routesService;
        private readonly IMapper _mapper;
        public RouteConsumer(IRoutesService routes, IMapper mapper)
        {
            _routesService = routes;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<RouteDTO> context)
        {
            Console.WriteLine("Message Received in event bus consumer!");
            Console.WriteLine(context);
            await _routesService.PostRoute(ConvertToDTO(context));
        }

        private RouteDTO ConvertToDTO(ConsumeContext<RouteDTO> context) 
        {
            var dto = new RouteDTO();
            var segments = new List<SegmentDTO>();

            dto.Id = context.Message.Id;
            dto.PriceTotal = context.Message.PriceTotal;

            foreach (var seg in context.Message.Segments)
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
}
