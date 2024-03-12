using MassTransit;
using PaymentService.Services.Test;
using Rekeningrijden.RabbitMq;

namespace PaymentService.Models.RabbitMq
{
    public class PaymentConsumer : IConsumer<TestRabbitMqClass>
    {
        private readonly ITestService _testService;
        public PaymentConsumer(ITestService service) 
        {
            _testService = service;
        
        }

        public async Task Consume(ConsumeContext<TestRabbitMqClass> context)
        {
            TestRabbitMqClass response = new TestRabbitMqClass()
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
            };

            await _testService.rabbitMqTest(response);
            //await Console.Out.WriteLineAsync(context.Message.Id);
        }
    }
}
