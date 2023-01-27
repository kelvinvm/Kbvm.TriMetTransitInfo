using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class AddressResultsDidNotMeetConfidenceMinimumException : GeocodeErrorException
	{
		public decimal Confidence { get; set; }

		// constructors...
		#region AddressResultsDidNotMeetConfidenceMinimumException()
		/// <summary>
		/// Constructs a new AddressResultsDidNotMeetConfidenceMinimumException.
		/// </summary>
		public AddressResultsDidNotMeetConfidenceMinimumException(string inputAddress, string outputJson, decimal confidence) : this(inputAddress, outputJson, confidence, null, null)
		{ 
			Confidence = confidence;
		}
		
		#endregion
		#region AddressResultsDidNotMeetConfidenceMinimumException(string message)
		/// <summary>
		/// Constructs a new AddressResultsDidNotMeetConfidenceMinimumException.
		/// </summary>
		/// <param name="message">The exception message</param>
		public AddressResultsDidNotMeetConfidenceMinimumException(string inputAddress, string outputJson, decimal confidence, string message) : this(inputAddress, outputJson, confidence, message, null) { }
		
		#endregion
		#region AddressResultsDidNotMeetConfidenceMinimumException(string message, Exception innerException)
		/// <summary>
		/// Constructs a new AddressResultsDidNotMeetConfidenceMinimumException.
		/// </summary>
		/// <param name="message">The exception message</param>
		/// <param name="innerException">The inner exception</param>
		public AddressResultsDidNotMeetConfidenceMinimumException(string inputAddress, string outputJson, decimal confidence, string message, Exception innerException) : base(inputAddress, outputJson, message, innerException) { }
		
		#endregion
		#region AddressResultsDidNotMeetConfidenceMinimumException(SerializationInfo info, StreamingContext context)
		/// <summary>
		/// Serialization constructor.
		/// </summary>
		protected AddressResultsDidNotMeetConfidenceMinimumException(SerializationInfo info, StreamingContext context) : base(info, context) { }
		#endregion
	}
}
