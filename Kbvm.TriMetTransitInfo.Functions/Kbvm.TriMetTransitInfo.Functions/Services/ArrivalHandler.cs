using Kbvm.TriMetTransitInfo.Dto.Records;
using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
    public class ArrivalHandler : IArrivalHandler
    {
        private readonly IGetLatLong _geocoder;
        private readonly IGetStopInfo _stopInfo;
        private readonly IGetArrivalInfo _arrivalInfo;

        public ArrivalHandler(IGetLatLong geocoder, IGetStopInfo stopInfo, IGetArrivalInfo arrivalInfo)
        {
            _geocoder = geocoder ?? throw new ArgumentNullException(nameof(geocoder));
            _stopInfo = stopInfo ?? throw new ArgumentNullException(nameof(stopInfo));
            _arrivalInfo = arrivalInfo ?? throw new ArgumentNullException(nameof(arrivalInfo));
        }

        public async Task<Arrivals> GetArrivalsAsync(string address)
        {
            var coords = await _geocoder.GetCoordinatesFromAddressAsync(address);
            var stops = await _stopInfo.GetStopsNearLatLongAsync(coords);
            var stop = stops.ResultSet.Location.First();

            var arrivals = await _arrivalInfo.GetArrivalsAtStop(stop.LocId);
            return arrivals;
        }
    }
}
