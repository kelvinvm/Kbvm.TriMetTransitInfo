using Kbvm.TriMetTransitInfo.Dto.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Interfaces
{
	public interface IGetArrivalInfo
	{
		Task<Arrivals> GetArrivalsAtStop(int locId);
	}
}
