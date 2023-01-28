using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Web;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
	internal class PositionStackForwardGeoCode : IGetLatLong
	{
		private readonly HttpClient _httpClient;
		private readonly PositionStackConfig _config;

		public PositionStackForwardGeoCode(HttpClient httpClient, PositionStackConfig config)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		public async Task<GeoPoint> GetCoordinatesFromAddressAsync(string address)
		{
			var queryString = $"forward?access_key={_config.AccessKey}&query={HttpUtility.UrlEncode(address)}";

			var response = await _httpClient.GetAsync(queryString);
			var json = await response.Content.ReadAsStringAsync();

			var geocodeResults = json.Deserialize<ForwardGeocodeResult>();
			if (geocodeResults.Data == null || !geocodeResults.Data.Any())
				throw new GeocodeErrorException(address, json, "Unserializable result from HTTP response");

			var geocodeResult = geocodeResults.Data.First();
			if (geocodeResult.Confidence < 0.80M)
				throw new AddressResultsDidNotMeetConfidenceMinimumException(address, json, geocodeResult.Confidence);

			return new GeoPoint(geocodeResult.Longitude, geocodeResult.Latitude);
		}
	}

	internal record ForwardGeocodeResultItem( 
		decimal Latitude,
		decimal Longitude,
		string Type,
		decimal Confidence,
		string Number,
		string Street,
		string Region,
		string Locality,
		string Postal_Code
	);

	internal record ForwardGeocodeResult(IEnumerable<ForwardGeocodeResultItem> Data);
}
