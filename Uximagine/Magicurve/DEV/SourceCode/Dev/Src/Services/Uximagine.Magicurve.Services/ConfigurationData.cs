namespace Uximagine.Magicurve.Services
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
            /// Represents the name of the service implementation plug in.
            /// </summary>
            public const string ServiceImplementation = @"ServiceImpl";

            #endregion
        }

        #endregion

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
    }
}
