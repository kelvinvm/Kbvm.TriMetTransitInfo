using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
	internal class TriMetStopInfo : IGetStopInfo
	{
		private readonly HttpClient _httpClient;
		private readonly TriMetConfig _config;

		public TriMetStopInfo(HttpClient httpClient, TriMetConfig config)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		public async Task<Stops> GetStopsNearLatLongAsync(GeoPoint point)
		{
			var queryString = $"V1/stops" +
				$"?appID={_config.ApplicationId}" +
				$"&ll={point.Latitude},{point.Longitude}" +
				$"&meters={_config.DistanceToSearch}" +
				$"&showRouteDirs=true" +
				$"&json=true";

			var response = await _httpClient.GetAsync(queryString);
			var json = await response.Content.ReadAsStringAsync();

			var triMetStops = json.Deserialize<Stops>();
			if (triMetStops.ResultSet == null || triMetStops.ResultSet.Location == null || !triMetStops.ResultSet.Location.Any())
				throw new TriMetStopsErrorException(point, json);

			return triMetStops;
		}
	}
}
