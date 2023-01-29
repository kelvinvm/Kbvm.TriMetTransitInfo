using System;
using System.Collections.Generic;
using System.Linq;

namespace Kbvm.TriMetTransitInfo.Dto.Records
{
	public record Stop(string Desc, int LocId, decimal MetersDistant, IEnumerable<RouteInfo> Route);
}
