using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
	internal class TriMetArrivalInfo : IGetArrivalInfo
	{
		private readonly HttpClient _httpClient;
		private readonly TriMetConfig _config;

		public TriMetArrivalInfo(HttpClient httpClient, TriMetConfig config)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		public async Task<Arrivals> GetArrivalsAtStop(int locId)
		{
			var queryString = $"V2/arrivals" +
				$"?locIDs={locId}" +
				$"&appId={_config.ApplicationId}" +
				$"&json=true" +
				$"&showPosition=true" +
				$"&minutes={_config.ArrivalWindowInMinutes}";
			string json = string.Empty;

			try
			{
				var response = await _httpClient.GetAsync(queryString);
				json = await response.Content.ReadAsStringAsync();

				var arrivals = json.Deserialize<Arrivals>();
				if (arrivals.ResultSet == null || arrivals.ResultSet.Arrival == null || !arrivals.ResultSet.Arrival.Any())
					throw new TriMetArrivalsErrorException(locId, json);

				return arrivals;
			}
			catch (JsonException ex)
			{
				throw new TriMetArrivalsErrorException(locId, json, "Error while deserializing response JSON from TriMet", ex);
			}
		}
	}
}
