using Kbvm.TriMetTransitInfo.Dto.Records;
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
using System.Text;
using Kbvm.TriMetTransitInfo.Functions.Interfaces;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Kbvm.TriMetTransitInfo.Functions.Endpoints
{
    public class GetTriMetArrivalData
    {

        private IArrivalHandler _handler;

        public GetTriMetArrivalData(IArrivalHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        [FunctionName("GetTriMetArrivalData")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string address = req.Query["address"];
			Arrivals arrivals = await _handler.GetArrivalsAsync(address);

            return new OkObjectResult(arrivals);
        }
    }
}
