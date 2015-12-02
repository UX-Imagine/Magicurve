using System;

namespace Uximagine.Magicurve.Core.Diagnostics
{
    /// <summary>
    /// Contains operation data.
    /// </summary>
    [Serializable]
    public class Operation
    {
        #region Properties - Instance Members
        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get;
            set;
        }   
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> class.
        /// </summary>
        public Operation() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public Operation(string message) : this(DateTimeHelper.Now, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> class.
        /// </summary>
        /// <param name="timestamp">The time-stamp.</param>
        /// <param name="message">The message.</param>
        private Operation(DateTime timestamp, string message)
        {
            this.Timestamp = timestamp;
            this.Message = string.IsNullOrEmpty(message) ? string.Empty : message;
        }
    }
}
