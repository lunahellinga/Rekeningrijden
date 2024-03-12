using Microsoft.AspNetCore.Mvc;
using PaymentService.DTOs.Pricing;
using PaymentService.Services.Pricing;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPriceService _priceService;

        public PaymentController(IPriceService priceService)
        {
           _priceService = priceService;
        }

        [HttpGet]
        public async Task<string> Test()
        {
            return "payment controller 1";
        }

        [HttpGet("/getPrices")]
        public async Task<ActionResult<List<GetPricingDTO>>> Value()
        {
            try
            {
                List<GetPricingDTO> res = new List<GetPricingDTO>();
                res = await _priceService.GetAllPricings();
                if (res.Count == 0) throw new Exception();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost("/createPrices")]
        public async Task<ActionResult<CreatePricingDTO>> CreatePricing(CreatePricingDTO dto)
        {
            try
            {
                CreatePricingDTO response = await _priceService.CreatePricings(dto);
                return Ok(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("/putPrices")]
        public async Task<ActionResult<GetPricingDTO>> UpdateCar(UpdatePricingDTO dto)
        {
            try
            {
                GetPricingDTO response = await _priceService.UpdatePricings(dto);
                return Ok(response);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpDelete("/deletePrices")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            try
            {
                await _priceService.DeletePricings(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
