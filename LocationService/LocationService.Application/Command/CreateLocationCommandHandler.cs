using LocationService.Domain;
using MediatR;

namespace LocationService.Application.Handlers.Commands;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
{
	private readonly ILocationWriteRepository _repository;
    
	public CreateLocationCommandHandler(ILocationWriteRepository repository)
	{
		_repository = repository;
	}
    
	public async Task<Location> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
	{
		var location = new Location
		{
			Id = Guid.NewGuid(),
			Details = request.Details,
			Address = request.Address,
			Latitude = request.Latitude,
			Longitude = request.Longitude,
			CreatedAt = DateTime.UtcNow
		};
        
		return await _repository.CreateAsync(location, cancellationToken);
	}
}