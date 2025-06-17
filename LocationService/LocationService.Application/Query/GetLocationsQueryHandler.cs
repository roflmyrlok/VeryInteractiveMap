using MediatR;
using LocationService.Domain;

namespace LocationService.Application;

public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, (IEnumerable<Location> Locations, int TotalCount)>
{
	private readonly ILocationReadRepository _repository;
    
	public GetLocationsQueryHandler(ILocationReadRepository repository)
	{
		_repository = repository;
	}
    
	public async Task<(IEnumerable<Location> Locations, int TotalCount)> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
	{
		var locations = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
		var totalCount = await _repository.GetTotalCountAsync(cancellationToken);
		return (locations, totalCount);
	}
}