using System;
using System.Runtime.Serialization;

namespace Uximagine.Magicurve.Core.Reflection
{
    
    /// <summary>
    /// The refection exception.
    /// </summary>
    [Serializable]
    public class ReflectionException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionException"/> class.
        /// </summary>
        public ReflectionException()
            :this(Resource.ReflectionDefaultExceptionMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ReflectionException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ReflectionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ReflectionException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
