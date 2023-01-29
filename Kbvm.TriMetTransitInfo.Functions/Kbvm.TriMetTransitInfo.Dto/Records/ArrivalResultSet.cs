using System;
using System.Collections.Generic;
using System.Linq;

namespace Kbvm.TriMetTransitInfo.Dto.Records
{
	public record ArrivalResultSet(IEnumerable<Arrival> Arrival, IEnumerable<Location> Location);
}
