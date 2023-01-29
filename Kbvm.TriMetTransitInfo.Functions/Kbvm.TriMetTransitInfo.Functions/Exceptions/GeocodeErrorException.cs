using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kbvm.TriMetTransitInfo.Functions.Exceptions
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

		/// <summary>
		/// Constructs a new AddressNotFOundException.
		/// </summary>
		/// <param name="message">The exception message</param>
		public GeocodeErrorException(string inputAddress, string outputJson) : this(inputAddress, outputJson, null, null) { }

		#region AddressNotFOundException(SerializationInfo info, StreamingContext context)
		/// <summary>
		/// Serialization constructor.
		/// </summary>
		protected GeocodeErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
