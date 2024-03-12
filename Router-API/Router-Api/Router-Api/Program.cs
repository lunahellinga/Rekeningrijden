using MassTransit;
using Router_Api.Data;
using Router_Api.Models.RabbitMq;
using Router_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseMySql(builder.Configuration.GetConnectionString("AzureDeployment"), new MySqlServerVersion(new Version(5, 7, 31)));
});


builder.Services.AddScoped<IRouterApiService, RouterApiService>();

var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
builder.Services.AddMassTransit(mt => mt.AddMassTransit(x => {
    //x.UsingRabbitMq((cntxt, cfg) => {
    //    cfg.Host(rabbitMqSettings.Uri, c => {
    //        c.Username(rabbitMqSettings.UserName);
    //        c.Password(rabbitMqSettings.Password);
    //    });
    //});

    x.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(rabbitMqSettings.Uri, "/", c => {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });

        cfg.ConfigureEndpoints(ctx);
    });
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials
// app.UseHttpsRedirection();
app.MapHealthChecks("/");
// app.UseAuthorization();

app.MapControllers();

app.Run();
