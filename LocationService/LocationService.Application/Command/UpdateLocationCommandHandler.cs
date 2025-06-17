using LocationService.Domain;
using MediatR;


namespace LocationService.Application;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Location?>
{
	private readonly ILocationWriteRepository _repository;
    
	public UpdateLocationCommandHandler(ILocationWriteRepository repository)
	{
		_repository = repository;
	}
    
	public async Task<Location?> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
	{
		var existingLocation = await _repository.GetByIdAsync(request.Id, cancellationToken);
		if (existingLocation == null) return null;
        
		existingLocation.Details = request.Details;
		existingLocation.Address = request.Address;
		existingLocation.Latitude = request.Latitude;
		existingLocation.Longitude = request.Longitude;
		existingLocation.UpdatedAt = DateTime.UtcNow;
        
		return await _repository.UpdateAsync(existingLocation, cancellationToken);
	}
}