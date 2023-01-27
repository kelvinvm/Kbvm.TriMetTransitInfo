using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Net.Http;
using Kbvm.TriMetTransitInfo.Functions.Interfaces;

namespace Kbvm.TriMetTransitInfo.Functions.Endpoints
{
    public class GetTriMetArrivalData
    {
        private readonly IGetLatLong _geocoder;

		public GetTriMetArrivalData(IGetLatLong geocoder)
		{
			_geocoder = geocoder ?? throw new ArgumentNullException(nameof(geocoder));
		}

		[FunctionName("GetTriMetArrivalData")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string address = req.Query["address"];

            var coords = await _geocoder.GetCoordinatesFromAddressAsync(address);

            string responseMessage = $"Coords: {coords?.Latitude ?? -42M}, {coords?.Longitude ?? -42M}";

            return new OkObjectResult(responseMessage);
        }
    }
}
