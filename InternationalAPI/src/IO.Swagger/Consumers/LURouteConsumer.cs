using IO.Swagger.DTOS;
using IO.Swagger.Models;
using MassTransit;
using System.Threading.Tasks;

namespace IO.Swagger.Consumers
{
    /// <summary>
    /// 
    /// </summary>
    public class LURouteConsumer : IConsumer<LURouteDTO>
    {
        private readonly IRoutingService _routingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingService"></param>
        public LURouteConsumer(IRoutingService routingService)
        {
            _routingService = routingService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<LURouteDTO> context)
        {
            IRoute dto = new LURouteDTO();

            dto.Segments = context.Message.Segments;
            dto.PriceTotal = context.Message.PriceTotal;
            dto.Id = context.Message.Id;
            string cc = "LU";

            await _routingService.Processed(cc, dto);
        }
    }
}
