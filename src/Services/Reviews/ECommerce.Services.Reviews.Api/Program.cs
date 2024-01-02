using ECommerce.Services.Reviews.Api;
using ECommerce.Shared.Infrastructure;
using Microsoft.AspNetCore.Builder;

var service = new ReviewsService();

var builder = WebApplication.CreateBuilder();

builder.Services
    .AddInfrastructure(service.Policies);

service.Register(builder.Services);

var app = builder.Build();

service.Use(app);

app.UseInfrastructure();

app.Run();