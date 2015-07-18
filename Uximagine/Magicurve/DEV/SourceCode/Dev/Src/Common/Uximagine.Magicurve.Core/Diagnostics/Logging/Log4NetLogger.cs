using log4net;
using System;

namespace Uximagine.Magicurve.Core.Diagnostics.Logging
{
    /// <summary>
    /// The log 4 net logger.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        /// <summary>
        /// Logs the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Log(Type type, ErrorSeverity severity, string message, Exception exception)
        {
            // retrieve the log4net logger
            ILog log = log4net.LogManager.GetLogger(type);

            #region // make log entry

            // check the severity; log accordingly, if log level is enabled
            switch (severity)
            {
                case ErrorSeverity.Debug:
                    if (log.IsDebugEnabled)
                    {
                        log.Debug(message, exception);
                    }

                    break;
                case ErrorSeverity.Information:
                    if (log.IsInfoEnabled)
                    {
                        log.Info(message, exception);
                    }

                    break;
                case ErrorSeverity.Warning:
                    if (log.IsWarnEnabled)
                    {
                        log.Warn(message, exception);
                    }

                    break;
                case ErrorSeverity.Error:
                    if (log.IsErrorEnabled)
                    {
                        log.Error(message, exception);
                    }

                    break;
                case ErrorSeverity.Fatal:
                    if (log.IsFatalEnabled)
                    {
                        log.Fatal(message, exception);
                    }

                    break;
            }

            #endregion
        }
    }
}
