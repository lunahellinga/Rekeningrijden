﻿namespace ProfileMicroService.Models.RabbitMQ
{
    public class RabbitMqSettings
    {
        public string Uri { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
