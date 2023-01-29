using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kbvm.TriMetTransitInfo.Functions.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AddressResultsDidNotMeetConfidenceMinimumException : Exception
    {
        public string InputAddress { get; set; }
        public decimal Confidence { get; set; }

        // constructors...
        #region AddressResultsDidNotMeetConfidenceMinimumException()
        /// <summary>
        /// Constructs a new AddressResultsDidNotMeetConfidenceMinimumException.
        /// </summary>
        public AddressResultsDidNotMeetConfidenceMinimumException(string inputAddress, decimal confidence) : this(inputAddress, confidence, null, null) { }

        #endregion
        #region AddressResultsDidNotMeetConfidenceMinimumException(string message)
        /// <summary>
        /// Constructs a new AddressResultsDidNotMeetConfidenceMinimumException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public AddressResultsDidNotMeetConfidenceMinimumException(string inputAddress, decimal confidence, string message) : this(inputAddress, confidence, message, null) { }

        #endregion
        #region AddressResultsDidNotMeetConfidenceMinimumException(string message, Exception innerException)
        /// <summary>
        /// Constructs a new AddressResultsDidNotMeetConfidenceMinimumException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public AddressResultsDidNotMeetConfidenceMinimumException(string inputAddress, decimal confidence, string message, Exception innerException) : base(message, innerException) 
        {
            InputAddress = inputAddress;
            Confidence = confidence;
        }

        #endregion
        #region AddressResultsDidNotMeetConfidenceMinimumException(SerializationInfo info, StreamingContext context)
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        protected AddressResultsDidNotMeetConfidenceMinimumException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
