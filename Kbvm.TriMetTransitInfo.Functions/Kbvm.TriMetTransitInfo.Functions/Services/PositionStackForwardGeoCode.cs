using Kbvm.TriMetTransitInfo.Cache;
using Kbvm.TriMetTransitInfo.Functions.Classes;
using Kbvm.TriMetTransitInfo.Functions.Exceptions;
using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using Kbvm.TriMetTransitInfo.Dto.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
    internal class PositionStackForwardGeoCode : HttpQueryBaseClass<ForwardGeocodeResult>, IGetLatLong
	{
		private readonly HttpClient _httpClient;
		private readonly PositionStackConfig _config;
		private readonly ICache<ForwardGeocodeResult> _cache;

		public PositionStackForwardGeoCode(HttpClient httpClient, PositionStackConfig config, ICache<ForwardGeocodeResult>	cache) : base(cache, httpClient)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_config = config ?? throw new ArgumentNullException(nameof(config));
			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
		}

		public async Task<GeoPoint> GetCoordinatesFromAddressAsync(string address)
		{
			try
			{
				var queryString = $"forward?access_key={_config.AccessKey}&query={HttpUtility.UrlEncode(address)}";

				var geocodeResults = await RunQueryAsync(queryString, address);

				if (geocodeResults.Data == null || !geocodeResults.Data.Any())
					throw new GeocodeErrorException(address, string.Empty, "Unexpected JSON result");

				var geocodeResult = geocodeResults.Data.First();
				if (geocodeResult.Confidence < 0.80M)
					throw new AddressResultsDidNotMeetConfidenceMinimumException(address, geocodeResult.Confidence);

				return new GeoPoint(geocodeResult.Longitude, geocodeResult.Latitude);
			}
			catch (HttpQueryBaseException ex)
			{
				throw new GeocodeErrorException(address, ex.JsonString, ex.Message, ex);
			}
		}
	}
}
