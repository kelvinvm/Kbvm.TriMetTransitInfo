using System;
using System.Linq;

namespace Kbvm.TriMetTransitInfo.Dto.Records
{
	public record Arrival(bool Detoured, long Estimated, long Scheduled, string ShortSign)
	{
		public DateTime EstimatedTime => DateTimeOffset.FromUnixTimeMilliseconds(Estimated).DateTime.ToLocalTime();
		public DateTime ScheduledTime => DateTimeOffset.FromUnixTimeMilliseconds(Scheduled).DateTime.ToLocalTime();
		public TimeSpan RemainingTime => DateTime.Now.Subtract(EstimatedTime);
	}
}
