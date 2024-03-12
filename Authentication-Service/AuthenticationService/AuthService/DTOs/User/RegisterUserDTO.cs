namespace AuthService.DTOs.User
{
    public class RegisterUserDTO
    {
        public Guid AuthId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Adress { get; set; }

        public string Bio { get; set; }

    }
}
