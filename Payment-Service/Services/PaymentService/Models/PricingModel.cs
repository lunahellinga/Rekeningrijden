using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class PricingModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string PriceTitle { get; set; }

        [Required]
        public string PriceType { get; set; }

        [Required]
        public string ValueName { get; set; }

        [Required]
        public double ValueDescription { get; set; }
        //public List<test2>? Tests2 { get; set; } = null;

    }
}
