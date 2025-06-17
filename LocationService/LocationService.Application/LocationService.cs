using MediatR;
using LocationService.Domain;

namespace LocationService.Application;

public class LocationService : ILocationService
{
    private readonly IMediator _mediator;
    
    public LocationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<LocationResponse> GetLocationAsync(GetLocationRequest request)
    {
        try
        {
            var location = await _mediator.Send(new GetLocationQuery(request.Id));
            
            return new LocationResponse
            {
                Location = location ?? new Location(),
                Success = location != null,
                Message = location != null ? "Location found" : "Location not found"
            };
        }
        catch (Exception ex)
        {
            return new LocationResponse
            {
                Success = false,
                Message = $"Error retrieving location: {ex.Message}"
            };
        }
    }
    
    public async Task<LocationsResponse> GetLocationsAsync(GetLocationsRequest request)
    {
        try
        {
            var (locations, totalCount) = await _mediator.Send(new GetLocationsQuery(request.PageSize, request.PageNumber));
            
            return new LocationsResponse
            {
                Locations = locations.ToList(),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
        catch (Exception ex)
        {
            return new LocationsResponse
            {
                Locations = new List<Location>(),
                TotalCount = 0,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
    
    public async Task<LocationsResponse> SearchLocationsAsync(SearchLocationsRequest request)
    {
        try
        {
            var (locations, totalCount) = await _mediator.Send(new SearchLocationsQuery(
                request.Query,
                request.Latitude,
                request.Longitude,
                request.RadiusKm,
                request.PageSize,
                request.PageNumber));
            
            return new LocationsResponse
            {
                Locations = locations.ToList(),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
        catch (Exception ex)
        {
            return new LocationsResponse
            {
                Locations = new List<Location>(),
                TotalCount = 0,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
    
    public async Task<LocationResponse> CreateLocationAsync(CreateLocationRequest request)
    {
        try
        {
            var location = await _mediator.Send(new CreateLocationCommand(
                request.Details,
                request.Address,
                request.Latitude,
                request.Longitude));
            
            return new LocationResponse
            {
                Location = location,
                Success = true,
                Message = "Location created successfully"
            };
        }
        catch (Exception ex)
        {
            return new LocationResponse
            {
                Success = false,
                Message = $"Error creating location: {ex.Message}"
            };
        }
    }
    
    public async Task<LocationResponse> UpdateLocationAsync(UpdateLocationRequest request)
    {
        try
        {
            var location = await _mediator.Send(new UpdateLocationCommand(
                request.Id,
                request.Details,
                request.Address,
                request.Latitude,
                request.Longitude));
            
            return new LocationResponse
            {
                Location = location ?? new Location(),
                Success = location != null,
                Message = location != null ? "Location updated successfully" : "Location not found"
            };
        }
        catch (Exception ex)
        {
            return new LocationResponse
            {
                Success = false,
                Message = $"Error updating location: {ex.Message}"
            };
        }
    }
    
    public async Task<DeleteLocationResponse> DeleteLocationAsync(DeleteLocationRequest request)
    {
        try
        {
            var success = await _mediator.Send(new DeleteLocationCommand(request.Id));
            
            return new DeleteLocationResponse
            {
                Success = success,
                Message = success ? "Location deleted successfully" : "Location not found"
            };
        }
        catch (Exception ex)
        {
            return new DeleteLocationResponse
            {
                Success = false,
                Message = $"Error deleting location: {ex.Message}"
            };
        }
    }
}