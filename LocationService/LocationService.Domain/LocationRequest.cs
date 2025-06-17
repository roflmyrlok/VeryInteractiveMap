using System.Runtime.Serialization;

namespace LocationService.Domain;

[DataContract]
public class GetLocationRequest
{
	[DataMember(Order = 1)]
	public Guid Id { get; set; }
}

[DataContract]
public class GetLocationsRequest
{
	[DataMember(Order = 1)]
	public int PageSize { get; set; } = 10;
    
	[DataMember(Order = 2)]
	public int PageNumber { get; set; } = 1;
}

[DataContract]
public class SearchLocationsRequest
{
	[DataMember(Order = 1)]
	public string Query { get; set; } = string.Empty;
    
	[DataMember(Order = 2)]
	public double? Latitude { get; set; }
    
	[DataMember(Order = 3)]
	public double? Longitude { get; set; }
    
	[DataMember(Order = 4)]
	public double? RadiusKm { get; set; }
    
	[DataMember(Order = 5)]
	public int PageSize { get; set; } = 10;
    
	[DataMember(Order = 6)]
	public int PageNumber { get; set; } = 1;
}

[DataContract]
public class CreateLocationRequest
{
	[DataMember(Order = 1)]
	public ICollection<LocationDetail> Details { get; set; } = new List<LocationDetail>();
    
	[DataMember(Order = 2)]
	public string Address { get; set; } = string.Empty;
    
	[DataMember(Order = 3)]
	public double Latitude { get; set; }
    
	[DataMember(Order = 4)]
	public double Longitude { get; set; }
}

[DataContract]
public class UpdateLocationRequest
{
	[DataMember(Order = 1)]
	public Guid Id { get; set; }
    
	[DataMember(Order = 2)]
	public ICollection<LocationDetail> Details { get; set; } = new List<LocationDetail>();
    
	[DataMember(Order = 3)]
	public string Address { get; set; } = string.Empty;
    
	[DataMember(Order = 4)]
	public double Latitude { get; set; }
    
	[DataMember(Order = 5)]
	public double Longitude { get; set; }
}

[DataContract]
public class DeleteLocationRequest
{
	[DataMember(Order = 1)]
	public Guid Id { get; set; }
}