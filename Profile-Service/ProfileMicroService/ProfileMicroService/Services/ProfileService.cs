using ProfileMicroService.Data;
using ProfileMicroService.Models;
using Rekeningrijden.RabbitMq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProfileMicroService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly DataContext _dataContext;

        public ProfileService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task RegisterUser(RegisterUserDTO dto)
        {
            Profile profile = new Profile()
            {
                Id = Guid.NewGuid(),
                AuthId = dto.AuthId,
                Adress = dto.Adress,
                Bio = dto.Bio,
                UserName = dto.UserName,
                Name = dto.Name,
            };

            try
            {
                if (_dataContext.Profiles.Any(u => u.AuthId == dto.AuthId)) throw new Exception("AuthId already exists");

                _dataContext.Profiles.Add(profile);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
