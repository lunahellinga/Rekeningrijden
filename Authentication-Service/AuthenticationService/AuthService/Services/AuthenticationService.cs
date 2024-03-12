using AuthService.DTOs.Login;
using AuthService.DTOs.User;
using AuthService.Models.Token;
using System.Reflection;
using BCrypt.Net;
using MassTransit;
using AuthService.Data;
using AuthService.Models.User;

namespace AuthService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthenticationService(DataContext context, IConfiguration configuration, IPublishEndpoint endpoint)
        {
            _dataContext = context;
            _configuration = configuration;
            _publishEndpoint = endpoint;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            List<UserDTO> users = new List<UserDTO>();

            //try
            //{
            //    List<User> userlist = await _dataContext.users.ToListAsync();

            //    foreach (User user in userlist)
            //    {
            //        users.Add(new UserDTO(user.UserName));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            return users;
        }

        public async Task<CreateUserDTO> Register(CreateUserDTO register)
        {
            //code om te checken of alle benodigde data er is
            foreach (PropertyInfo pi in register.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(register);
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new InvalidOperationException("missing value");
                    }
                }
            }

            string password = BCrypt.Net.BCrypt.HashPassword(register.Password);
            Guid id = Guid.NewGuid();

            UserModel user = new UserModel()
            {
                Id = id,
                Email = register.Email,
                Password = password,
                Role = UserRole.NORMAL,
                DateEnrolled = DateTime.Now
            };

            RegisterUserDTO profile = new RegisterUserDTO()
            {
                Name = register.Name,
                UserName = register.UserName,
                Adress = register.Adress,
                AuthId = id,
                Bio = register.Bio,
            };

            try
            {
                if (_dataContext.Users.Any(u => u.Email == user.Email)) throw new ArgumentException("Email already exists");

                _dataContext.Users.Add(user);

                //messagebroker voor registeren profiel
                //await _publishEndpoint.Publish<RegisterUserDTO>(profile);

                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return register;
        }

        public async Task<TokenDTO> GenerateToken(LoginDTO dto)
        {
            UserModel user = _dataContext.Users.Single(u => u.Email == dto.Email);
            TokenDTO token = new TokenDTO();

            TokenManager manager = new TokenManager(_configuration);

            try
            {
                if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) throw new Exception("Email or password do not match");
                token.Token = manager.CreateToken(user).ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return token;
        }
    }
}
