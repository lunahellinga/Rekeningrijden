using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Services.Test;
using PaymentService.Models.RabbitMq;
using MassTransit;
using PaymentService.Services.Pricing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(5, 7, 31)));
    options.UseMySql(builder.Configuration.GetConnectionString("MigrationConnection"),
        new MySqlServerVersion(new Version(5, 7, 31)));
    //options.UseMySql(builder.Configuration.GetConnectionString("KubernetesConnection"), new MySqlServerVersion(new Version(5, 7, 31)));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IPriceService, PriceService>();

var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
builder.Services.AddMassTransit(mt => mt.AddMassTransit(x =>
{
    mt.AddConsumer<PaymentConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitMqSettings.Uri, c =>
        {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });
        cfg.ReceiveEndpoint("payment", c => { c.ConfigureConsumer<PaymentConsumer>(ctx); });
    });
}));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();