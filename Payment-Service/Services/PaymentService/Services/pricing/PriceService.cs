using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.DTOs.Pricing;
using PaymentService.Models;

namespace PaymentService.Services.Pricing
{
    public class PriceService : IPriceService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public PriceService(DataContext context, IMapper mapper)
        {
            _dataContext = context;
            _mapper = mapper;
        }

        public async Task<CreatePricingDTO> CreatePricings(CreatePricingDTO Dto)
        {
            try
            {
                PricingModel post = _mapper.Map<PricingModel>(Dto);
                post.Id = Guid.NewGuid();
                _dataContext.Pricing.Add(post);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Dto;
        }

        public async Task<bool> DeletePricings(Guid id)
        {
            try
            {
                _dataContext.Remove(_dataContext.Pricing.Single(k => k.Id == id));
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetPricingDTO>> GetAllPricings()
        {
            List<GetPricingDTO> response = new List<GetPricingDTO>();

            try
            {
                List<PricingModel> pricings = await _dataContext.Pricing.ToListAsync();

                response = pricings.Select(t => _mapper.Map<PricingModel, GetPricingDTO>(t)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        public async Task<GetPricingDTO> UpdatePricings(UpdatePricingDTO Dto)
        {
            GetPricingDTO res = new();

            try
            {
                PricingModel? before = _dataContext.Find<PricingModel>(Dto.Id);

                before.PriceTitle = Dto.PriceTitle;
                before.PriceType = Dto.PriceType;
                before.ValueName = Dto.ValueName;
                before.ValueDescription = Dto.ValueDescription;

                _dataContext.Pricing.Update(before);
                await _dataContext.SaveChangesAsync();

                res = _mapper.Map<GetPricingDTO>(before);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }
    }
}
