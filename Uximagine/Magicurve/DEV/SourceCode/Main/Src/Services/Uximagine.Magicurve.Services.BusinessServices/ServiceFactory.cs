namespace Uximagine.Magicurve.Services.BusinessServices
{
    /// <summary>
    /// Get the service instance.
    /// </summary>
    public class ServiceFactory
    {
        /// <summary>
        /// Gets the processing service.
        /// </summary>
        /// <returns>
        /// The service.
        /// </returns>
        public IProcessingService GetProcessingService()
        {
            return new ProcessingService();
        }
    }
}