using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kbvm.TriMetTransitInfo.Functions.Exceptions
{
    /// <summary>
	/// 
	/// </summary>
	[Serializable]
    public class HttpQueryBaseException : Exception
    {
        public string JsonString { get; set; }
        // constructors...
        #region HttpQueryBaseException()
        /// <summary>
        /// Constructs a new HttpQueryBaseException.
        /// </summary>
        public HttpQueryBaseException(string jsonString) : this(jsonString, null, null) { }
        #endregion

        #region HttpQueryBaseException(string message)
        /// <summary>
        /// Constructs a new HttpQueryBaseException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public HttpQueryBaseException(string jsonString, string message) : this(jsonString, message, null) { }
        #endregion

        #region HttpQueryBaseException(string message, Exception innerException)
        /// <summary>
        /// Constructs a new HttpQueryBaseException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public HttpQueryBaseException(string jsonString, string message, Exception innerException) : base(message, innerException)
        {
            JsonString = jsonString;
        }
        #endregion

        #region HttpQueryBaseException(SerializationInfo info, StreamingContext context)
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        protected HttpQueryBaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
