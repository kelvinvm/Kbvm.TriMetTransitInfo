using Kbvm.TriMetTransitInfo.Dto.Records;

namespace Kbvm.TriMetTransitInfo.Cache
{
	public class ForwardGeocodeCache : ICache<ForwardGeocodeResultItem>
	{
		private readonly ICosmosHandler _cosmosHandler;
		public TimeSpan Duration => TimeSpan.FromDays(30);

		public ForwardGeocodeCache(ICosmosHandler cosmosHandler)
		{
			_cosmosHandler = cosmosHandler ?? throw new ArgumentNullException(nameof(cosmosHandler));
		}

		public Task<ForwardGeocodeResultItem> AddAsync(string key, ForwardGeocodeResultItem obj)
		{
			throw new NotImplementedException();
		}

		public Task<bool> TryGetAsync(string key, out ForwardGeocodeResultItem item)
		{
			
		}
	}
}