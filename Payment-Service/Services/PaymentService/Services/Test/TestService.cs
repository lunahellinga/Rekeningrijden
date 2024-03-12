using PaymentService.Data;
using PaymentService.DTOs.Pricing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rekeningrijden.RabbitMq;

namespace PaymentService.Services.Test
{
    public class TestService : ITestService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TestService(DataContext context, IMapper mapper) 
        {
            _dataContext = context;
            _mapper = mapper;
        }

        public async Task<List<GetPricingDTO>> getAllTests()
        {
            List<GetPricingDTO> response = new List<GetPricingDTO>();
                      
            try
            {
                List<Models.PricingModel> tests = await _dataContext.Pricing.ToListAsync();

                response = tests.Select(t => _mapper.Map<Models.PricingModel, GetPricingDTO>(t)).ToList();
            }
            catch (Exception ex)
            {
                response = null;
            }
            return response;
        }

        public async Task<string> rabbitMqTest(TestRabbitMqClass rabbit)
        {
            return rabbit.Id + " " + rabbit.Name;
        }




    }
}
