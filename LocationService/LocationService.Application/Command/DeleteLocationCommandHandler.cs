using MediatR;


namespace LocationService.Application;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
{
	private readonly ILocationWriteRepository _repository;
    
	public DeleteLocationCommandHandler(ILocationWriteRepository repository)
	{
		_repository = repository;
	}
    
	public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
	{
		return await _repository.DeleteAsync(request.Id, cancellationToken);
	}
}