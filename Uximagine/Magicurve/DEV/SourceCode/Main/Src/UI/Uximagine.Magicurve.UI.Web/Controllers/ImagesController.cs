#region Imports

using System.Web.Http;
using Uximagine.Magicurve.DataTransfer.Responses;
using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.Services;
using Uximagine.Magicurve.UI.Web.Common;

#endregion

namespace Uximagine.Magicurve.UI.Web.Controllers
{
    using System.Threading.Tasks;

    using Uximagine.Magicurve.DataTransfer.Common;

    /// <summary>
    /// The API controller.
    /// </summary>
    public class ImagesController : ApiController
    {
        /// <summary>
        /// Gets the edges.
        /// </summary>
        /// <param name="controlsRequest">The controls request.</param>
        /// <returns>
        /// The edge image.
        /// </returns>
        [Route("api/images/controls")]
        [HttpGet]
        public async Task<ImagesResult> GetControls(ControlsRequest controlsRequest)
        {
            string imgPath = controlsRequest.FileUrl;

            IProcessingService service = ServiceFactory.GetProcessingService();
            ProcessRequestDto request = new ProcessRequestDto { ImagePath = imgPath };
            ProcessResponseDto response = await service.ProcessImage(request);

            IFileService fileService = ServiceFactory.GetFileService();

            FileSaveResponse fileResponse = await fileService.SaveFile(response.ImageResult, "result.jpg");

            response.ImageResult.Dispose();

            var json = new ImagesResult()
            {
                Controls = response.Controls,
                SourceImageWidth = response.SourceImageWidth,
                SourceImageHeight = response.SourceImageWidth,
                Url = fileResponse.Uri
            };

            return json;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="controlsRequest">The controls request.</param>
        /// <returns>
        /// The image path.
        /// </returns>
        [Route("api/images/result")]
        [HttpPost]
        public async Task<IHttpActionResult> GetImage(ControlsRequest controlsRequest)
        {
            string imgPath = controlsRequest.FileUrl;

            IProcessingService service = ServiceFactory.GetProcessingService();
            ProcessRequestDto request = new ProcessRequestDto { ImagePath = imgPath };
            ProcessResponseDto response = await service.ProcessImage(request);

            IFileService fileService = ServiceFactory.GetFileService();

            FileSaveResponse fileResponse = await fileService.SaveFile(response.ImageResult, "result.jpg");

            response.ImageResult.Dispose();

            var json = new ImagesResult()
            {
                Controls = response.Controls,
                SourceImageWidth = response.SourceImageWidth,
                SourceImageHeight = response.SourceImageHeight,
                Url = fileResponse.Uri
            };

            return this.Json(json);
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="controlsResult">
        /// The controls result.
        /// </param>
        /// <returns>
        /// The image path.
        /// </returns>
        [Route("api/images/code")]
        [HttpPost]
        public IHttpActionResult GenerateCode(ControlsResult controlsResult)
        {
            IProcessingService service = ServiceFactory.GetProcessingService();
            GenerateCodeRequest request = new GenerateCodeRequest()
            {
                Controls = controlsResult.Controls,
                ImageWidth = controlsResult.ImageWidth,
                ImageHeight = controlsResult.ImageHeight
            };

            GenerateCodeResponse response = service.GenerateCode(request);

            return this.Json(new CodeResult()
            {
                Code = response.Code
            });
        }

        /// <summary>
        /// Downloads this instance.
        /// </summary>
        /// <param name="controlsResult">
        /// The controls result
        /// </param>
        /// <returns>
        /// The file
        /// </returns>
        [Route("api/images/download")]
        [HttpPost]
        public async Task<IHttpActionResult> DownloadCode(ControlsResult controlsResult)
        {
            if (controlsResult != null)
            {
                IProcessingService service = ServiceFactory.GetProcessingService();
                GenerateCodeRequest request = new GenerateCodeRequest()
                {
                    Controls = controlsResult.Controls,
                    ImageWidth = controlsResult.ImageWidth,
                    ImageHeight = controlsResult.ImageHeight
                };

                GenerateCodeResponse response = service.GenerateCode(request);

                string fileName = ConfigurationData.DownloadFileName;

                IFileService fileService = ServiceFactory.GetFileService();

                FileSaveResponse fileSaveResponse = await fileService.SaveFile(response.Code, fileName);

                string url = fileSaveResponse.Uri;

                return this.Json(new
                {
                    url = url
                });
            }

            return null;
        }
    }
}
