using Uximagine.Magicurve.DataTransfer.Requests;

namespace Uximagine.Magicurve.Services.Client
{
    using Uximagine.Magicurve.DataTransfer.Responses;
    using Uximagine.Magicurve.Services.BusinessServices;

    /// <summary>
    /// The processing service.
    /// </summary>
    public class ProcessigService : IProcessingService
    {
        /// <summary>
        /// Gets the processed image URL.
        /// </summary>
        /// <param name="requestDto">The requestDto.</param>
        /// <returns>
        /// The URL.
        /// </returns>
        public ProcessResponseDto GetEdgeProcessedImageUrl(ProcessRequestDto requestDto)
        {
            IProcessingService service = this.GetService();

            return service.GetEdgeProcessedImageUrl(requestDto);
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <returns>
        /// The processing service.
        /// </returns>
        public IProcessingService GetService()
        {
            ServiceFactory factory = new ServiceFactory();

            return factory.GetProcessingService();
        }

    }
}
