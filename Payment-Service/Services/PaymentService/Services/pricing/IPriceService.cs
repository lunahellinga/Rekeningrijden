using PaymentService.DTOs.Pricing;

namespace PaymentService.Services.Pricing
{
    public interface IPriceService
    {
        public Task<List<GetPricingDTO>> GetAllPricings();

        public Task<CreatePricingDTO> CreatePricings(CreatePricingDTO Dto);

        public Task<GetPricingDTO> UpdatePricings(UpdatePricingDTO Dto);

        public Task<bool> DeletePricings(Guid id);
    }
}
