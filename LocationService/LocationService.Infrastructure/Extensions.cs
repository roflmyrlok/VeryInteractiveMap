using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LocationService.Application;


namespace LocationService.Infrastructure;

public static class Extensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		// Add DbContext
		services.AddDbContext<LocationDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

		// Add repositories
		services.AddScoped<ILocationReadRepository, LocationReadRepository>();
		services.AddScoped<ILocationWriteRepository, LocationWriteRepository>();

		return services;
	}
}