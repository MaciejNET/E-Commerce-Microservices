using Convey.MessageBrokers.RabbitMQ;
using ECommerce.Services.Users.Api;
using ECommerce.Shared.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var service = new UsersService();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(service.Policies);

service.Register(builder.Services);

var app = builder.Build();

service.Use(app);

app.UseInfrastructure();

app.Run();