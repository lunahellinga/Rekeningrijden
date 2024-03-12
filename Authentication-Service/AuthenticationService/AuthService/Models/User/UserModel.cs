using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.User
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public DateTime DateEnrolled { get; set; }

        public UserModel()
        {

        }

    }
}
