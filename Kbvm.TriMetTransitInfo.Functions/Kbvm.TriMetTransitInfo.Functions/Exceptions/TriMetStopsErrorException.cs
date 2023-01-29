using Kbvm.TriMetTransitInfo.Dto.Records;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kbvm.TriMetTransitInfo.Functions.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TriMetStopsErrorException : Exception
    {
        public GeoPoint InputGeoPoint { get; set; }
        public string OutputJson { get; set; }
        private const string DefaultMessage = "Unserializable result from HTTP result";

        // constructors...
        #region TriMetStopsErrorException()
        /// <summary>
        /// Constructs a new TriMetStopsErrorException.
        /// </summary>
        public TriMetStopsErrorException(GeoPoint inputGeoPoint, string outputJson) : this(inputGeoPoint, outputJson, DefaultMessage, null) { }
        #endregion

        #region TriMetStopsErrorException(string message)
        /// <summary>
        /// Constructs a new TriMetStopsErrorException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public TriMetStopsErrorException(GeoPoint inputGeoPoint, string outputJson, string message) : this(inputGeoPoint, outputJson, message, null) { }
        #endregion

        #region TriMetStopsErrorException(string message, Exception innerException)
        /// <summary>
        /// Constructs a new TriMetStopsErrorException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public TriMetStopsErrorException(GeoPoint inputGeoPoint, string outputJson, string message, Exception innerException) : base(message, innerException)
        {
            InputGeoPoint = inputGeoPoint;
            OutputJson = outputJson;
        }
        #endregion

        #region TriMetStopsErrorException(SerializationInfo info, StreamingContext context)
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        protected TriMetStopsErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
