#region Imports

using System.Threading.Tasks;
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
            DetectControlsUnitOfWork work = new DetectControlsUnitOfWork { ImagePath = requestDto.ImagePath };

            this.DoWork(work);

            ProcessResponseDto response = new ProcessResponseDto
            {
                Controls = work.Controls,
                ImageResult = work.ImageResult,
                SourceImageWidth = work.ImageWidth,
                SourceImageHeight = work.ImageHeight
            };

            return response;
        }

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The generated code.
        /// </returns>
        public GenerateCodeResponse GenerateCode(GenerateCodeRequest request)
        {
            GenerateCodeResponse response = new GenerateCodeResponse();

            GenerateCodeUnitOfWork work = new GenerateCodeUnitOfWork()
            {
                Controls = request.Controls,
                ImageWidth = request.ImageWidth,
                ImageHeight = request.ImageHeight
            };

            this.DoWork(work);

            response.Code = work.Code;

            return response;
        }

        /// <summary>
        /// Trains this instance.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        public async Task Train(TrainRequest request)
        {
            await Task.Run(() =>
            {
                TrainDataUnitOfWork work = new TrainDataUnitOfWork(true)
                {
                    ForceTraining = request.ForceTraining
                };

                this.DoWork(work);
            });
        }
    }
}
