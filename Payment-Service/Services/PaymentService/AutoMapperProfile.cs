using AutoMapper;
using PaymentService.DTOs.Pricing;
using PaymentService.Models;

namespace PaymentService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<PricingModel, GetPricingDTO>();
            CreateMap<GetPricingDTO, PricingModel>();

            CreateMap<PricingModel, CreatePricingDTO>();
            CreateMap<CreatePricingDTO, PricingModel>();

            CreateMap<PricingModel, UpdatePricingDTO>();
            CreateMap<UpdatePricingDTO, PricingModel>();
        }
    }
}
