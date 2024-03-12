namespace PaymentService.DTOs.Pricing
{
    public class CreatePricingDTO
    {
        public string PriceTitle { get; set; }
        public string PriceType { get; set; }
        public string ValueName { get; set; }
        public double ValueDescription { get; set; }
    }
}
