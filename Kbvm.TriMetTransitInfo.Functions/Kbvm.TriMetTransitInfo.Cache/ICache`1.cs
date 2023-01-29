namespace Kbvm.TriMetTransitInfo.Cache
{
	public interface ICache<T> where T : class
	{
		TimeSpan Duration { get; }

		Task<T> AddAsync(string key, T obj);
		Task<bool> TryGetAsync(string key, out T item);
	}
}