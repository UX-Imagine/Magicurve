namespace Uximagine.Magicurve.Services
{
    /// <summary>
    /// Get the service instance.
    /// </summary>
    public class ServiceFactory
    {
        /// <summary>
        /// The service.
        /// </summary>
        private IProcessingService service;

        /// <summary>
        /// Gets the processing service.
        /// </summary>
        /// <returns>
        /// The service.
        /// </returns>
        public IProcessingService GetProcessingService()
        {
            if (this.service == null)
            {
                this.service = new ProcessingService();
            }

            return this.service;
        }
    }
}