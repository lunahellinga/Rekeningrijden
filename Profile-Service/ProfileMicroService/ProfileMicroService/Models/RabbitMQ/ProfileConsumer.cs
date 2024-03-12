using MassTransit;
using ProfileMicroService.Services;
using Rekeningrijden.RabbitMq;

namespace ProfileMicroService.Models.RabbitMQ
{
    public class ProfileConsumer : IConsumer<RegisterUserDTO>
    {
        private readonly IProfileService _profileService;

        public ProfileConsumer(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task Consume(ConsumeContext<RegisterUserDTO> context)
        {
            RegisterUserDTO response = new RegisterUserDTO()
            {
                AuthId = context.Message.AuthId,
                Adress = context.Message.Adress,
                UserName = context.Message.UserName,
                Name = context.Message.Name,
                Bio = context.Message.Bio,
            };

            await _profileService.RegisterUser(response);

            //await Console.Out.WriteLineAsync(context.Message.Id);
        }
    }
}
