using System.Configuration;

namespace Uximagine.Magicurve.Services.BusinessServices
{
    /// <summary>
    /// Provides access to configuration data.
    /// </summary>
    internal static class ConfigurationData
    {
        #region Classes

        /// <summary>
        /// Contains plug in names.
        /// </summary>
        private static class PluginNames
        {
            #region Constants

            /// <summary>
            /// Represents the name of the service implementation plugin.
            /// </summary>
            public const string ServiceImplementation = @"ServiceImpl";

            #endregion
        }

        #endregion

        #region Constants

        /// <summary>
        /// Represents a value indicating whether plaintext secrets are to be used, 
        /// by default.
        /// </summary>
        private const bool MustUsePlaintextSecretsByDefault = false;

        /// <summary>
        /// Represents a value indicating whether read operations must be serialized, 
        /// by default.
        /// </summary>
        private const bool MustSerializeReadOperationsByDefault = false;

#if DEBUG
        /// <summary>
        /// Represents a value indicating whether operational performance must be logged,
        /// by default.
        /// </summary>
        private const bool MustLogOperationalPerformanceByDefault = true;
#else
		/// <summary>
		/// Represents a value indicating whether operational performance must be logged,
		/// by default.
		/// </summary>
		private const bool MustLogOperationalPerformanceByDefault = false;
#endif

        #endregion

        #region Properties - Static Member

        /// <summary>
        /// Gets a value indicating whether plaintext secrets are to be used.
        /// </summary>
        /// <value>
        /// <c>true</c> if plaintext secrets are to be used; 
        /// otherwise, <c>false</c>.
        /// </value>
        public static bool MustUsePlaintextSecrets
        {
            get
            {
                return ConfigurationData.MustUsePlaintextSecretsByDefault;
            }
        }

        /// <summary>
        /// Gets a value indicating whether read operations must be serialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if read operations must be serialized; 
        /// otherwise, <c>false</c>.
        /// </value>
        public static bool MustSerializeReadOperations
        {
            get
            {
                return ConfigurationData.MustSerializeReadOperationsByDefault;
            }
        }

        /// <summary>
        /// Gets a value indicating whether operational performance must be logged.
        /// </summary>
        /// <value>
        /// <c>true</c> if operational performance must be logged; 
        /// otherwise, <c>false</c>.
        /// </value>
        public static bool MustLogOperationalPerformance
        {
            get
            {
                return ConfigurationData.MustLogOperationalPerformanceByDefault;
            }
        }

        /// <summary>
        /// Gets the name of the service implementation plug-in.
        /// </summary>
        /// <value>
        /// The name of the service implementation plug-in.
        /// </value>
        public static string ServiceImplementationPluginName
        {
            get
            {
                return ConfigurationData.PluginNames.ServiceImplementation;
            }
        }

        /// <summary>
        /// Gets the minimum size of the control.
        /// </summary>
        /// <value>
        /// The minimum size of the control.
        /// </value>
        public static int MinControlSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["minSize"]);
            }
        }

        /// <summary>
        /// Gets the size of the sample.
        /// </summary>
        /// <value>
        /// The size of the sample.
        /// </value>
        public static int SampleSize
        {
            get { return 32; }
        }

        #endregion
    }
}
