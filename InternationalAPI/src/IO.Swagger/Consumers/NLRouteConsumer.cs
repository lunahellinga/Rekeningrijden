using IO.Swagger.DTOS;
using IO.Swagger.Models;
using MassTransit;
using System.Threading.Tasks;

namespace IO.Swagger.Consumers
{
    /// <summary>
    /// 
    /// </summary>
    public class NLRouteConsumer : IConsumer<NLRouteDTO>
    {
        private readonly IRoutingService _routingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingService"></param>
        public NLRouteConsumer(IRoutingService routingService)
        {
            _routingService = routingService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<NLRouteDTO> context)
        {
            IRoute dto = new NLRouteDTO();

            dto.Segments = context.Message.Segments;
            dto.PriceTotal = context.Message.PriceTotal;
            dto.Id = context.Message.Id;
            string cc = "NL";

            await _routingService.Processed(cc, dto);
        }
    }
}
