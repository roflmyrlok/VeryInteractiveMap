using LocationService.Domain;

namespace LocationService.Application;

public interface ILocationWriteRepository
{
	Task<Location> CreateAsync(Location location, CancellationToken cancellationToken = default);
	Task<Location?> UpdateAsync(Location location, CancellationToken cancellationToken = default);
	Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}