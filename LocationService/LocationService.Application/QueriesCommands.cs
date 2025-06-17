using LocationService.Domain;
using MediatR;

namespace LocationService.Application;


public record GetLocationQuery(Guid Id) : IRequest<Location?>;


public record GetLocationsQuery(int PageSize, int PageNumber) 
	: IRequest<(IEnumerable<Location> Locations, int TotalCount)>;



public record SearchLocationsQuery(
	string Query,
	double? Latitude,
	double? Longitude,
	double? RadiusKm,
	int PageSize,
	int PageNumber) : IRequest<(IEnumerable<Location> Locations, int TotalCount)>;



public record CreateLocationCommand(
    ICollection<LocationDetail> Details,
    string Address,
    double Latitude,
    double Longitude) : IRequest<Location>;


public record UpdateLocationCommand(
	Guid Id,
	ICollection<LocationDetail> Details,
	string Address,
	double Latitude,
	double Longitude) : IRequest<Location?>;


public record DeleteLocationCommand(Guid Id) : IRequest<bool>;