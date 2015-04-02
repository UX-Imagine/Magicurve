using System;
namespace Uximagine.Magicurve.Core.Diagnostics.Logging
{
    /// <summary>
    /// Interface for different loggers.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Log(Type type, ErrorSeverity severity, string message, Exception exception);
    }
}
