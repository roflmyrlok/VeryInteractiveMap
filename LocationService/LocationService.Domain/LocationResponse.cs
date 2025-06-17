using System.Runtime.Serialization;

namespace LocationService.Domain;


[DataContract]
public class LocationResponse
{
	[DataMember(Order = 1)]
	public Location Location { get; set; } = null!;
    
	[DataMember(Order = 2)]
	public bool Success { get; set; }
    
	[DataMember(Order = 3)]
	public string Message { get; set; } = string.Empty;
}

[DataContract]
public class LocationsResponse
{
	[DataMember(Order = 1)]
	public ICollection<Location> Locations { get; set; } = new List<Location>();
    
	[DataMember(Order = 2)]
	public int TotalCount { get; set; }
    
	[DataMember(Order = 3)]
	public int PageNumber { get; set; }
    
	[DataMember(Order = 4)]
	public int PageSize { get; set; }
}

[DataContract]
public class DeleteLocationResponse
{
	[DataMember(Order = 1)]
	public bool Success { get; set; }
    
	[DataMember(Order = 2)]
	public string Message { get; set; } = string.Empty;
}