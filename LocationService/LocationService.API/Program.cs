using DotNetEnv;
using LocationService.Application;
using LocationService.Infrastructure;
using ProtoBuf.Grpc.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();


builder.Services.AddCodeFirstGrpc();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ILocationService).Assembly));
builder.Services.AddScoped<ILocationService, LocationService.Application.LocationService>();


builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


app.MapGrpcService<LocationService.Application.LocationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();