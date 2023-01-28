using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kbvm.TriMetTransitInfo.Functions.Interfaces
{
	public interface IGetArrivalInfo
	{
		Task<Arrivals> GetArrivalsAtStop(int locId);
	}

	public interface IGetStopInfo
	{
		Task<Stops> GetStopsNearLatLongAsync(GeoPoint point);
	}

	public record Stops(StopResultSet ResultSet);
	public record StopResultSet(IEnumerable<Stop> Location);
	public record Stop(string Desc, int LocId, decimal MetersDistant, IEnumerable<RouteInfo> Route);
	public record RouteInfo(string Desc, int Route, string RouteColor, string RouteSubType, string Type);

	public record Arrivals(ArrivalResultSet ResultSet);
	public record ArrivalResultSet(IEnumerable<Arrival> Arrival, IEnumerable<Location> Location);
	public record Location(string Desc, string Dir);
	public record Arrival(bool Detoured, long Estimated, long Scheduled, string ShortSign)
	{
		public DateTime EstimatedTime => DateTimeOffset.FromUnixTimeMilliseconds(Estimated).DateTime.ToLocalTime();
		public DateTime ScheduledTime => DateTimeOffset.FromUnixTimeMilliseconds(Scheduled).DateTime.ToLocalTime();
		public TimeSpan RemainingTime => DateTime.Now.Subtract(EstimatedTime);
	}

}
