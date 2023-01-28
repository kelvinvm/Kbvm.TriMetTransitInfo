using System;
using System.Linq;
using System.Text.Json;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
	internal static class Extensions
	{
		public static T Deserialize<T>(this string json)
		{
			return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}
	}
}
