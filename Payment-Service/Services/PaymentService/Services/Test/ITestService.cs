using PaymentService.DTOs.Pricing;
using Rekeningrijden.RabbitMq;

namespace PaymentService.Services.Test
{
    public interface ITestService
    {
        public Task<List<GetPricingDTO>> getAllTests();

        public Task<string> rabbitMqTest(TestRabbitMqClass rabbit);
    }
}
