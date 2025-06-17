using LocationService.Domain;
using MediatR;


namespace LocationService.Application;

public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Location?>
{
	private readonly ILocationReadRepository _repository;
    
	public GetLocationQueryHandler(ILocationReadRepository repository)
	{
		_repository = repository;
	}
    
	public async Task<Location?> Handle(GetLocationQuery request, CancellationToken cancellationToken)
	{
		return await _repository.GetByIdAsync(request.Id, cancellationToken);
	}
}