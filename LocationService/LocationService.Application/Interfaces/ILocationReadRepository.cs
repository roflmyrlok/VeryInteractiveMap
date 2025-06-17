using LocationService.Domain;

namespace LocationService.Application;

public interface ILocationReadRepository
{
	Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	Task<IEnumerable<Location>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
	Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
	Task<IEnumerable<Location>> SearchAsync(
		string query, 
		double? latitude, 
		double? longitude, 
		double? radiusKm, 
		int pageNumber, 
		int pageSize, 
		CancellationToken cancellationToken = default);
	Task<int> GetSearchCountAsync(
		string query, 
		double? latitude, 
		double? longitude, 
		double? radiusKm, 
		CancellationToken cancellationToken = default);
}