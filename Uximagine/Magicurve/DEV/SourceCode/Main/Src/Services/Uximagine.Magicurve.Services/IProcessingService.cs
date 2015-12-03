using System.ServiceModel;
using System.Threading.Tasks;
using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.DataTransfer.Responses;

namespace Uximagine.Magicurve.Services
{
    /// <summary>
    ///     The processing service.
    /// </summary>
    [ServiceContract]
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
        [OperationContract]
        ProcessResponseDto ProcessImage(ProcessRequestDto requestDto);

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The generated code.
        /// </returns>
        [OperationContract]
        GenerateCodeResponse GenerateCode(GenerateCodeRequest request);

        /// <summary>
        /// Trains this instance.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        [OperationContract]
        Task Train(TrainRequest request);
    }
}