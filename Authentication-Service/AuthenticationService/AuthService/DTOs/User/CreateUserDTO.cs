namespace AuthService.DTOs.User
{
    public class CreateUserDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Bio { get; set; }
    }
}
