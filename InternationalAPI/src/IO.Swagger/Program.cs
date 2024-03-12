using System;
using System.IO;
using System.Text.Json.Serialization;
using IO.Swagger.Consumers;
using IO.Swagger.Filters;
using IO.Swagger.Models.RabbitMq;
using IO.Swagger.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MvcJsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("0.4.0", new OpenApiInfo
    {
        Version = "0.4.0",
        Title = "Rekeningrijden Belgium API  v3",
        Description = "Rekeningrijden Belgium API (.NET Core 7.0)",
        Contact = new OpenApiContact()
        {
            Name = "Swagger Codegen Contributors",
            Url = new Uri("https://github.com/swagger-api/swagger-codegen"),
        },
    });
    c.CustomSchemaIds(type => type.FullName);
    c.IncludeXmlComments(
        $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{builder.Environment.ApplicationName}.xml");

    // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
    c.OperationFilter<GeneratePathParamsValidationFilter>();
});

var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
builder.Services.AddMassTransit(mt => mt.AddMassTransit(x => {

    mt.AddConsumer<NLRouteConsumer>();
    mt.AddConsumer<LURouteConsumer>();

    x.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(rabbitMqSettings.Uri, c => {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });
        cfg.ReceiveEndpoint("NLRoute", c =>
        {
            c.ConfigureConsumer<NLRouteConsumer>(ctx);

        });
        cfg.ReceiveEndpoint("LURoute", c =>
        {
            c.ConfigureConsumer<LURouteConsumer>(ctx);
        });
    });
}));

builder.Services.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.Configure<MvcJsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddScoped<IRoutingService, RoutingService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/0.4.0/swagger.json", "Rekeningrijden Belgium API")
);

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials
    
// app.UseHttpsRedirection();
// app.UseForwardedHeaders();
// app.MapHealthChecks("/");
app.MapHealthChecks("/");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});

app.MapControllers();

app.Run();