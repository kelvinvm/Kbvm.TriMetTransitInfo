using System;
using System.Collections.Generic;
using System.Linq;

namespace Kbvm.TriMetTransitInfo.Dto.Records
{
	public record StopResultSet(IEnumerable<Stop> Location);
}
