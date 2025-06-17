using System.ServiceModel;
using LocationService.Domain;

namespace LocationService.Application;

[ServiceContract]
public interface ILocationService
{
	[OperationContract]
	Task<LocationResponse> GetLocationAsync(GetLocationRequest request);
    
	[OperationContract]
	Task<LocationsResponse> GetLocationsAsync(GetLocationsRequest request);
    
	[OperationContract]
	Task<LocationsResponse> SearchLocationsAsync(SearchLocationsRequest request);
    
	[OperationContract]
	Task<LocationResponse> CreateLocationAsync(CreateLocationRequest request);
    
	[OperationContract]
	Task<LocationResponse> UpdateLocationAsync(UpdateLocationRequest request);
    
	[OperationContract]
	Task<DeleteLocationResponse> DeleteLocationAsync(DeleteLocationRequest request);
}