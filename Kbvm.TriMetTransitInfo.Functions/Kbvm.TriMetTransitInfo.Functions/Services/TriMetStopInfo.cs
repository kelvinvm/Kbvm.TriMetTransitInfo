using Kbvm.TriMetTransitInfo.Cache;
using Kbvm.TriMetTransitInfo.Dto.Records;
using Kbvm.TriMetTransitInfo.Functions.Classes;
using Kbvm.TriMetTransitInfo.Functions.Exceptions;
using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
    internal class TriMetStopInfo : HttpQueryBaseClass<Stops>, IGetStopInfo
	{
		private readonly TriMetConfig _config;

		public TriMetStopInfo(ICache<Stops> cache, HttpClient httpClient, TriMetConfig config) : base(cache, httpClient)
		{
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		public async Task<Stops> GetStopsNearLatLongAsync(GeoPoint point)
		{
			try
			{
				var queryString = $"V1/stops" +
					$"?appID={_config.ApplicationId}" +
					$"&ll={point.Latitude},{point.Longitude}" +
					$"&meters={_config.DistanceToSearch}" +
					$"&showRouteDirs=true" +
					$"&json=true";

				var triMetStops = await RunQueryAsync(queryString, point.ToString());
				if (triMetStops.ResultSet == null || triMetStops.ResultSet.Location == null || !triMetStops.ResultSet.Location.Any())
					throw new TriMetStopsErrorException(point, string.Empty, "Unexpected JSON result.");

				return triMetStops;
			}
			catch (HttpQueryBaseException ex)
			{
				throw new TriMetStopsErrorException(point, ex.JsonString, ex.Message, ex);
			}
		}
	}
}
