using LocationService.Domain;
using MediatR;


namespace LocationService.Application;

public class SearchLocationsQueryHandler : IRequestHandler<SearchLocationsQuery, (IEnumerable<Location> Locations, int TotalCount)>
{
	private readonly ILocationReadRepository _repository;
    
	public SearchLocationsQueryHandler(ILocationReadRepository repository)
	{
		_repository = repository;
	}
    
	public async Task<(IEnumerable<Location> Locations, int TotalCount)> Handle(SearchLocationsQuery request, CancellationToken cancellationToken)
	{
		var locations = await _repository.SearchAsync(
			request.Query, 
			request.Latitude, 
			request.Longitude, 
			request.RadiusKm, 
			request.PageNumber, 
			request.PageSize, 
			cancellationToken);
            
		var totalCount = await _repository.GetSearchCountAsync(
			request.Query, 
			request.Latitude, 
			request.Longitude, 
			request.RadiusKm, 
			cancellationToken);
            
		return (locations, totalCount);
	}
}