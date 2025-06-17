using System.Runtime.Serialization;

namespace LocationService.Domain;

[DataContract]
public class LocationDetail
{
	[DataMember(Order = 1)]
	public Guid Id { get; set; }
	[DataMember(Order = 2)]
	public Guid LocationId { get; set; }
	[DataMember(Order = 3)]
	public string PropertyName { get; set; }
	[DataMember(Order = 4)]
	public string PropertyValue { get; set; }
	public Location Location { get; set; }
}