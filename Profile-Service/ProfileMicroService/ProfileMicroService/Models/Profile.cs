using System.ComponentModel.DataAnnotations;

namespace ProfileMicroService.Models
{
    public class Profile
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AuthId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string Bio { get; set; }
    }
}
