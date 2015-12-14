namespace Uximagine.Magicurve.UI.Web.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Http;

    using Uximagine.Magicurve.DataTransfer.Common;
    using Uximagine.Magicurve.DataTransfer.Requests;
    using Uximagine.Magicurve.DataTransfer.Responses;
    using Uximagine.Magicurve.Services;
    using Uximagine.Magicurve.UI.Web.Common;

    /// <summary>
    ///     The API controller.
    /// </summary>
    public class ImagesController : ApiController
    {
        /// <summary>
        ///     Downloads this instance.
        /// </summary>
        /// <param name="controlsResult">
        ///     The controls result
        /// </param>
        /// <returns>
        ///     The file
        /// </returns>
        [Route("api/images/download")]
        [HttpPost]
        public async Task<IHttpActionResult> DownloadCode(ControlsResult controlsResult)
        {
            if (controlsResult != null)
            {
                IProcessingService service = ServiceFactory.GetProcessingService();
                GenerateCodeRequest request = new GenerateCodeRequest
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

                if (HttpContext.Current.Request.IsLocal)
                {
                    url = this.Url.Content(url);
                }

                return this.Json(new { url });
            }

            return null;
        }

        /// <summary>
        ///     Gets the image.
        /// </summary>
        /// <param name="controlsResult">
        ///     The controls result.
        /// </param>
        /// <returns>
        ///     The image path.
        /// </returns>
        [Route("api/images/code")]
        [HttpPost]
        public IHttpActionResult GenerateCode(ControlsResult controlsResult)
        {
            IProcessingService service = ServiceFactory.GetProcessingService();
            GenerateCodeRequest request = new GenerateCodeRequest
                                              {
                                                  Controls = controlsResult.Controls, 
                                                  ImageWidth = controlsResult.ImageWidth, 
                                                  ImageHeight = controlsResult.ImageHeight
                                              };

            GenerateCodeResponse response = service.GenerateCode(request);

            return this.Json(new CodeResult { Code = response.Code });
        }

        /// <summary>
        ///     Gets the edges.
        /// </summary>
        /// <param name="controlsRequest">The controls request.</param>
        /// <returns>
        ///     The edge image.
        /// </returns>
        [Route("api/images/controls")]
        [HttpGet]
        public async Task<ImagesResult> GetControls(ControlsRequest controlsRequest)
        {
            string imgPath = controlsRequest.FileUrl;

            if (this.Request.IsLocal())
            {
                imgPath = HostingEnvironment.MapPath(imgPath);
            }

            IProcessingService service = ServiceFactory.GetProcessingService();
            ProcessRequestDto request = new ProcessRequestDto { ImagePath = imgPath };
            ProcessResponseDto response = await service.ProcessImage(request);

            IFileService fileService = ServiceFactory.GetFileService();

            FileSaveResponse fileResponse = await fileService.SaveFile(response.ImageResult, "result.jpg");

            response.ImageResult.Dispose();

            string url = fileResponse.Uri;

            if (HttpContext.Current.Request.IsLocal)
            {
                url = this.Url.Content(url);
            }

            ImagesResult json = new ImagesResult
                                    {
                                        Controls = response.Controls, 
                                        SourceImageWidth = response.SourceImageWidth, 
                                        SourceImageHeight = response.SourceImageWidth, 
                                        Url = url
                                    };

            return json;
        }

        /// <summary>
        ///     Gets the image.
        /// </summary>
        /// <param name="controlsRequest">The controls request.</param>
        /// <returns>
        ///     The image path.
        /// </returns>
        [Route("api/images/result")]
        [HttpPost]
        public async Task<IHttpActionResult> GetImage(ControlsRequest controlsRequest)
        {
            string imgPath = controlsRequest.FileUrl;

            if (this.Request.IsLocal())
            {
                imgPath = HostingEnvironment.MapPath(imgPath);
            }

            IProcessingService service = ServiceFactory.GetProcessingService();
            ProcessRequestDto request = new ProcessRequestDto { ImagePath = imgPath };
            ProcessResponseDto response = await service.ProcessImage(request);

            IFileService fileService = ServiceFactory.GetFileService();

            FileSaveResponse fileResponse = await fileService.SaveFile(response.ImageResult, "result.jpg");

            response.ImageResult.Dispose();

            string url = fileResponse.Uri;

            if (HttpContext.Current.Request.IsLocal)
            {
                url = this.Url.Content(url);
            }

            ImagesResult json = new ImagesResult
                                    {
                                        Controls = response.Controls, 
                                        SourceImageWidth = response.SourceImageWidth, 
                                        SourceImageHeight = response.SourceImageHeight, 
                                        Url = url
                                    };

            return this.Json(json);
        }
    }
}