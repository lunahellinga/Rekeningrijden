using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentService.Models
{
    public class test2
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey("test")]
        public int? testid { get; set; } = null;

        public PricingModel? Test { get; set; } = null;


    }
}
