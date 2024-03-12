using AuthService.DTOs.Login;
using AuthService.DTOs.User;

namespace AuthService.Services
{
    public interface IAuthenticationService
    {
        public Task<List<UserDTO>> GetAllUsers();

        public Task<CreateUserDTO> Register(CreateUserDTO userDTO);

        public Task<TokenDTO> GenerateToken(LoginDTO dto);

    }
}
