using Kbvm.TriMetTransitInfo.Cache;
using Kbvm.TriMetTransitInfo.Dto.Records;
using Kbvm.TriMetTransitInfo.Functions.Classes;
using Kbvm.TriMetTransitInfo.Functions.Exceptions;
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
    internal class TriMetArrivalInfo : HttpQueryBaseClass<Arrivals>, IGetArrivalInfo
	{
		private readonly TriMetConfig _config;

		public TriMetArrivalInfo(ICache<Arrivals> cache, HttpClient httpClient, TriMetConfig config) : base(cache, httpClient)
		{
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

			try
			{
				Arrivals arrivals = await RunQueryAsync(queryString, locId.ToString());

				if (arrivals.ResultSet == null || arrivals.ResultSet.Arrival == null || !arrivals.ResultSet.Arrival.Any())
					throw new TriMetArrivalsErrorException(locId, string.Empty, "Unexpected JSON result");

				return arrivals;
			}
			catch (HttpQueryBaseException ex)
			{
				throw new TriMetArrivalsErrorException(locId, ex.JsonString, ex.Message, ex);
			}
		}
	}
}
