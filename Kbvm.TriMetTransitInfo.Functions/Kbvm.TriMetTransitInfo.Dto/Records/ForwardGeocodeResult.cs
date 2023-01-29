using System;
using System.Collections.Generic;
using System.Linq;

namespace Kbvm.TriMetTransitInfo.Dto.Records
{
	public record ForwardGeocodeResult(IEnumerable<ForwardGeocodeResultItem> Data);
}
