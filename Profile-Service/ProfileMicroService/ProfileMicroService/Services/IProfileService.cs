using Rekeningrijden.RabbitMq;

namespace ProfileMicroService.Services
{
    public interface IProfileService
    {
        public Task RegisterUser(RegisterUserDTO dto);
    }
}
