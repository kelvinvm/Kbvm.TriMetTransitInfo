using Kbvm.TriMetTransitInfo.Cache;
using Kbvm.TriMetTransitInfo.Functions.Exceptions;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Classes
{
    internal class HttpQueryBaseClass<T> where T : class
    {
        private readonly ICache<T> _cache;
        private readonly HttpClient _httpClient;

        public HttpQueryBaseClass(ICache<T> cache, HttpClient httpClient)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        protected virtual async Task<T> RunQueryAsync(string queryString, string key)
        {
            var json = string.Empty;

            try
            {
                var normalizedKey = key.ToLower().Trim();
                if (await _cache.TryGetAsync(normalizedKey, out T item))
                    return item;

                var response = await _httpClient.GetAsync(queryString);
                json = await response.Content.ReadAsStringAsync();
                T newItem = json.Deserialize<T>();
                await _cache.AddAsync(normalizedKey, newItem);

                return newItem;
            }
            catch (JsonException ex)
            {
                throw new HttpQueryBaseException(json, $"Couldn't deserialize JSON of type {typeof(T).Name}", ex);
            }
            catch(Exception ex)
            {
                throw new HttpQueryBaseException(string.Empty, $"Query failed unexpectedly", ex);
            }
        }
    }
}
