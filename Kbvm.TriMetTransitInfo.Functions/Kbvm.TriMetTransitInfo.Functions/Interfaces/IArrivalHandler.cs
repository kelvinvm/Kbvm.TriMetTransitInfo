using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Interfaces
{
    public interface IArrivalHandler
    {
        Task<Arrivals> GetArrivalsAsync(string address);
    }
}
