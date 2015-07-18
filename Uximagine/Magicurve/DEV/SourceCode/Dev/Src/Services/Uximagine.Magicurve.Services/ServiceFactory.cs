namespace Uximagine.Magicurve.Services
{
    using Uximagine.Magicurve.Core.Reflection;

    /// <summary>
    /// Get the service instance.
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// Gets the processing service.
        /// </summary>
        /// <returns>
        /// The service.
        /// </returns>
        public static IProcessingService GetProcessingService()
        {
            return ObjectFactory.GetInstance<IProcessingService>(
				ConfigurationData.ServiceImplementationPluginName);
        }
    }
}