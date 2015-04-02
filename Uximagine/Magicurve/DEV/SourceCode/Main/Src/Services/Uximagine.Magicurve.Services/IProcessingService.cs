using System.ServiceModel;
using Uximagine.Magicurve.DataTransfer.Requests;

namespace Uximagine.Magicurve.Services
{
    using Uximagine.Magicurve.DataTransfer.Responses;

    /// <summary>
    /// The processing service.
    /// </summary>
    [ServiceContract(Namespace = "http://ux-imagine.com")]
    public interface IProcessingService
    {
        /// <summary>
        /// Gets the processed image URL.
        /// </summary>
        /// <param name="requestDto">The requestDto.</param>
        /// <returns>
        /// The URL.
        /// </returns>
        [OperationContract]
        ProcessResponseDto GetEdgeProcessedImageUrl(ProcessRequestDto requestDto);
    }
}
