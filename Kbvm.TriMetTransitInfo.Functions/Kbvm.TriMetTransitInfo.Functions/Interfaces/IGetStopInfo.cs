using Kbvm.TriMetTransitInfo.Dto.Records;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Interfaces
{
	public interface IGetStopInfo
	{
		Task<Stops> GetStopsNearLatLongAsync(GeoPoint point);
	}
}
