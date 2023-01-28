using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kbvm.TriMetTransitInfo.Functions.Services
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class GeocodeErrorException : Exception
	{
		public string InputAddress { get; set; }
		public string OutputJson { get; set; }

		// constructors...
		#region AddressNotFOundException()
		/// <summary>
		/// Constructs a new AddressNotFOundException.
		/// </summary>
		/// 
		public GeocodeErrorException(string inputAddress, string outputJson, string message, Exception innerException) : base(message, innerException) 
		{ 
			InputAddress = inputAddress;
			OutputJson = outputJson;
		}
		#endregion
		
		#region AddressNotFOundException(string message)
		/// <summary>
		/// Constructs a new AddressNotFOundException.
		/// </summary>
		/// <param name="message">The exception message</param>
		public GeocodeErrorException(string inputAddress, string outputJson, string message) : this(inputAddress, outputJson, message, null) { }		
		#endregion
		
		#region AddressNotFOundException(SerializationInfo info, StreamingContext context)
		/// <summary>
		/// Serialization constructor.
		/// </summary>
		protected GeocodeErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
		#endregion
	}
}
