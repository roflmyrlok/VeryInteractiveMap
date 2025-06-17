using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;

namespace Location.Domain;

[DataContract]
public class Location
{
	[DataMember(Order = 1)]
	public Guid Id { get; set; }
	[DataMember(Order = 2)]
	public ICollection<LocationDetail> Details { get; set; } = new List<LocationDetail>();
	[DataMember(Order = 3)]
	public string Address { get; set; }
	[DataMember(Order = 4)]
	public double Latitude { get; set; }
	[DataMember(Order = 5)]
	public double Longitude { get; set; }
	[DataMember(Order = 6)]
	public DateTime CreatedAt { get; set; }
	[DataMember(Order = 7)]
	public DateTime? UpdatedAt { get; set; }

}