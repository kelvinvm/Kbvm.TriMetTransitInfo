using Kbvm.TriMetTransitInfo.Dto.Records;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Interfaces
{
	public interface IGetLatLong
	{
		Task<GeoPoint> GetCoordinatesFromAddressAsync(string address);
	}
}
