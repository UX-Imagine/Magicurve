#region Imports
using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.DataTransfer.Responses;
using Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork;

#endregion

namespace Uximagine.Magicurve.Services.BusinessServices
{
    /// <summary>
    /// The processing business service
    /// </summary>
    public class ProcessingService : BusinessService, IProcessingService
    {
        /// <summary>
        /// Gets the processed image URL.
        /// </summary>
        /// <param name="requestDto">
        /// The requestDto.
        /// </param>
        /// <returns>
        /// The URL.
        /// </returns>
        public ProcessResponseDto ProcessImage(ProcessRequestDto requestDto)
        {
            ProcessResponseDto response = new ProcessResponseDto();

            DetectEdgesUnitOfWork work = new DetectEdgesUnitOfWork { ImagePath = requestDto.ImagePath };

            this.DoWork(work);

            response.Controls = work.Controls;
            response.ImageResult = work.ImageResult;

            return response;
        }

        /// <summary>
        /// Trains this instance.
        /// </summary>
        public void Train()
        {
            TrainDataUnitOfWork work = new TrainDataUnitOfWork(true);
            this.DoWork(work);
        }
    }
}
