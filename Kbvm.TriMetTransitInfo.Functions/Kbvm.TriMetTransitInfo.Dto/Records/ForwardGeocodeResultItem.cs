using System;
using System.Linq;

namespace Kbvm.TriMetTransitInfo.Dto.Records
{
	public record ForwardGeocodeResultItem(
		decimal Latitude,
		decimal Longitude,
		string Type,
		decimal Confidence,
		string Number,
		string Street,
		string Region,
		string Locality,
		string Postal_Code
	);
}
