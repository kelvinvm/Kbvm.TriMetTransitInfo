using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kbvm.TriMetTransitInfo.Functions.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TriMetArrivalsErrorException : Exception
    {
        public int LocationId { get; set; }
        public string OutputJson { get; set; }
        private const string DefaultMessage = "Unserializable result from HTTP result";

        // constructors...
        #region TriMetArrivalsErrorException()
        /// <summary>
        /// Constructs a new TriMetArrivalsErrorException.
        /// </summary>
        public TriMetArrivalsErrorException(int locationId, string outputJson) : this(locationId, outputJson, DefaultMessage, null) { }
        #endregion

        #region TriMetArrivalsErrorException(string message)
        /// <summary>
        /// Constructs a new TriMetArrivalsErrorException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public TriMetArrivalsErrorException(int locationId, string outputJson, string message) : this(locationId, outputJson, message, null) { }
        #endregion

        #region TriMetArrivalsErrorException(string message, Exception innerException)
        /// <summary>
        /// Constructs a new TriMetArrivalsErrorException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public TriMetArrivalsErrorException(int locationId, string outputJson, string message, Exception innerException) : base(message, innerException)
        {
            LocationId = locationId;
            OutputJson = outputJson;
        }
        #endregion

        #region TriMetArrivalsErrorException(SerializationInfo info, StreamingContext context)
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        protected TriMetArrivalsErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
