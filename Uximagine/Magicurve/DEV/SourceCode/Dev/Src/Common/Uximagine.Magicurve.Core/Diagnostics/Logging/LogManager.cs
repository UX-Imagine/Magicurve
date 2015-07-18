using System;

namespace Uximagine.Magicurve.Core.Diagnostics.Logging
{
    /// <summary>
    /// The log manager.
    /// </summary>
    public static class LogManager
    {

        /// <summary>
        /// Contains well-known keys for custom log data.
        /// </summary>
        public static class CustomData
        {
        #region Constants

			/// <summary>
			/// Represents the key identifying the client IP address.
			/// </summary>
			public const string ClientIPAddress = @"clientip";

			/// <summary>
			/// Represents the key identifying the server IP address.
			/// </summary>
			public const string ServerIPAddress = @"serverip";

			/// <summary>
			/// Represents the key identifying the source application name.
			/// </summary>
			public const string SourceAppName = @"sourceapp";

			/// <summary>
			/// Represents the key identifying the source application version.
			/// </summary>
			public const string SourceAppVersion = @"sourceappver";

            /// <summary>
            /// Represents the key identifying the logging application.
            /// </summary>
            public const string LoggingApp = @"loggingapp";

			/// <summary>
			/// Represents the key identifying the logging application version.
			/// </summary>
			public const string LoggingAppVersion = @"loggingappver";

			#endregion

		}

        /// <summary>
        /// Sets up the given key/value data for logging.
        /// </summary>
        /// <param name="key">
        /// The key identifying the data for logging.
        /// </param>
        /// <param name="value">
        /// The value of the log entry.
        /// </param>
        public static void SetupLogData(string key, string value)
        {
            log4net.ThreadContext.Properties[key] = value;
        }

        /// <summary>
		/// Creates a log entry on all configured loggers, based on the given 
		/// message of the specified severity, originating from the
		/// given type.
		/// </summary>
		/// <param name="type">
		/// The type from which the log request originated.
		/// </param>
		/// <param name="severity">
		/// The severity of the log entry.
		/// </param>
		/// <param name="message">
		/// The message to be logged.
		/// </param>
		public static void Log(Type type, ErrorSeverity severity, object message)
		{
			// validate message text
			string messageText = (message != null) ? message.ToString() : string.Empty;

			// invoke overload
			LogManager.Log(type, severity, messageText);
		}

		/// <summary>
		/// Creates a log entry on all configured loggers, based on the given 
		/// message of the specified severity, originating from the
		/// given type.
		/// </summary>
		/// <param name="type">
		/// The type from which the log request originated.
		/// </param>
		/// <param name="severity">
		/// The severity of the log entry.
		/// </param>
		/// <param name="message">
		/// The message to be logged.
		/// </param>
		public static void Log(Type type, ErrorSeverity severity, string message)
		{
			// invoke overload
			LogManager.Log(type, severity, message, null);
		}

		/// <summary>
		/// Creates a log entry on all configured loggers, based on the given 
		/// message and/or exception of the specified severity, originating from the
		/// given type.
		/// </summary>
		/// <param name="type">
		/// The type from which the log request originated.
		/// </param>
		/// <param name="severity">
		/// The severity of the log entry.
		/// </param>
		/// <param name="message">
		/// The message to be logged.
		/// </param>
		/// <param name="exception">
		/// The exception to be logged.
		/// </param>
		public static void Log(
            Type type, 
            ErrorSeverity severity, 
            string message,
			Exception exception)
		{
			// invoke helper
			LogManager.CreateLogEntry(type, severity, message, exception);
		}

		/// <summary>
		/// Creates a log entry on all configured loggers, based on the given 
		/// message and/or exception of the specified severity, originating from the
		/// given type.
		/// </summary>
		/// <param name="type">
		/// The type from which the log request originated.
		/// </param>
		/// <param name="severity">
		/// The severity of the log entry.
		/// </param>
		/// <param name="message">
		/// The message to be logged.
		/// </param>
		/// <param name="exception">
		/// The exception to be logged.
		/// </param>
		private static void CreateLogEntry(
            Type type, 
            ErrorSeverity severity, 
            string message,
			Exception exception)
		{
			// retrieve collection of loggers
			LoggerCollection loggers = LogManager.GetLoggers();

			// iterate through loggers
			foreach (ILogger logger in loggers)
			{
				try
				{
					// create log entry
					logger.Log(type, severity, message, exception);
				}
				catch
				{
					// sink exception
				}
			}
		}

        /// <summary>
        /// Gets the loggers.
        /// </summary>
        /// <returns>
        /// The logger collection.
        /// </returns>
        private static LoggerCollection GetLoggers()
        {
            LoggerCollection loggers = new LoggerCollection();
            loggers.Add(new Log4NetLogger());
            return loggers;
        }
    }
}
