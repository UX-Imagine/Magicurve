using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.DataTransfer.Responses;

namespace Uximagine.Magicurve.Services
{
    /// <summary>
    ///     The processing service.
    /// </summary>
    public interface IProcessingService
    {
        /// <summary>
        ///     Gets the processed image URL.
        /// </summary>
        /// <param name="requestDto">
        /// The requestDto.
        /// </param>
        /// <returns>
        ///     The URL.
        /// </returns>
        ProcessResponseDto ProcessImage(ProcessRequestDto requestDto);
    }
}