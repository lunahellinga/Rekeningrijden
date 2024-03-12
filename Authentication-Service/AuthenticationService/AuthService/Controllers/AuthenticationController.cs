using AuthService.DTOs.Login;
using AuthService.DTOs.User;
using AuthService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _authService;
        private readonly IConfiguration _configuration;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthenticationController(IAuthenticationService authService, IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _authService = authService;
            _configuration = configuration;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<string>> getAllUsers()
        {
            try
            {
                List<UserDTO> res = await _authService.GetAllUsers();
                if (res.Count == 0) return NotFound();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/register", Name = "RegisterUser")]
        public async Task<ActionResult<CreateUserDTO>> createUser(CreateUserDTO dto)
        {
            try
            {
                CreateUserDTO res = await _authService.Register(dto);
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/login", Name = "Login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO dto)
        {
            TokenDTO token = new TokenDTO();

            try
            {
                token = await _authService.GenerateToken(dto);
                if (token.Token == "") return BadRequest();
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/validate", Name = "ValidateUser")]
        [Authorize]
        public async Task<ActionResult<string>> validateUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userID = getUserID(identity);
            return Ok("user Validated!");
        }

        [HttpGet("/validate/admin", Name = "ValidateAdmin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<string>> validateAdmin()
        {
            return Ok("Admin Validated!");
        }

        [HttpPost("/rabbitMq")]
        public async Task<ActionResult<RegisterUserDTO>> RabbitMq(RegisterUserDTO dto)
        {
            await _publishEndpoint.Publish<RegisterUserDTO>(dto);
            return Ok("message sent");
        }

        private string getUserID(ClaimsIdentity identity)
        {
            string auth = "Authorization";
            string Id = string.Empty;

            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                try
                {
                    Id = claims.FirstOrDefault(x => x.Type == "ID").Value;
                }
                catch (Exception)
                {
                    throw new Exception("Token Not available");
                }
            }
            return Id;
        }
    }
}