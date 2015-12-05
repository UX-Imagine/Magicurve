#region Imports
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;
using Uximagine.Magicurve.DataTransfer.Responses;
using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.Services;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.UI.Web.Common;
using Uximagine.Magicurve.UI.Web.Models;

#endregion

namespace Uximagine.Magicurve.UI.Web.Controllers
{
    /// <summary>
    /// The API controller.
    /// </summary>
    public class ImagesController : ApiController
    {
        /// <summary>
        /// Gets the edges.
        /// </summary>
        /// <returns>
        /// The edge image.
        /// </returns>
        [Route("api/images/controls")]
        [HttpGet]
        public ControlsResult GetControls()
        {
            string imgPath = Path.Combine(HostingEnvironment.MapPath("~/Content/Images/Upload"), "upload.jpg");

            IProcessingService service = ServiceFactory.GetProcessingService();
            ProcessRequestDto request = new ProcessRequestDto { ImagePath = imgPath };
            ProcessResponseDto response = service.ProcessImage(request);

            var json = new ControlsResult()
            {
                Controls = response.Controls,
                ImageWidth = response.SourceImageWidth,
                ImageHeight = response.SourceImageWidth
            };

            response.ImageResult.Save(filename: HostingEnvironment.MapPath("~/Content/images/test2.png"));
            response.ImageResult.Dispose();

            return json;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>
        /// The image path.
        /// </returns>
        [Route("api/images/result")]
        [HttpGet]
        public IHttpActionResult GetImage()
        {
            var path = Path.Combine(HostingEnvironment.MapPath("~/Content/Images/Upload"), "upload.jpg");

            IProcessingService service = ServiceFactory.GetProcessingService();
            ProcessRequestDto request = new ProcessRequestDto { ImagePath = path };
            ProcessResponseDto response = service.ProcessImage(request);
            
            var savePath = HostingEnvironment.MapPath("~/Content/images/result.png");

            if (savePath != null)
            {
                response.ImageResult.Save(savePath);
                response.ImageResult.Dispose();
            }

            return this.Json(new ImagesResult()
                                {
                                    Url = "/Content/images/result.png",
                                    Controls = response.Controls,
                                    SourceImageWidth = response.SourceImageWidth,
                                    SourceImageHeight = response.SourceImageHeight
            });
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="controlsResult">The controls result.</param>
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
                ImageWidth = controlsResult.ImageWidth
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
        public IHttpActionResult DownloadCode(ControlsResult controlsResult)
        {
            if (controlsResult != null)
            {
                IProcessingService service = ServiceFactory.GetProcessingService();
                GenerateCodeRequest request = new GenerateCodeRequest()
                {
                    Controls = controlsResult.Controls,
                    ImageWidth = controlsResult.ImageWidth
                };

                GenerateCodeResponse response = service.GenerateCode(request);

                string fileName = ConfigurationData.DownloadFileName;

                var path = Path.Combine(HostingEnvironment.MapPath(ConfigurationData.DownloadDirectory), fileName);

                File.WriteAllText(@path, response.Code);

                string url = Url.Content(ConfigurationData.DownloadDirectory + "/" + fileName);

                return this.Json(new 
                {
                    url = url
                });
            }

            return null;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <returns> 
        /// Get API/values/5   
        /// </returns>
        [Route("api/images/{id}")]
        public List<Control> GetById(int id)
        {
            var result = new List<Control>();
            string imgPath;

            switch (id)
            {
                case 1:
                    imgPath = HostingEnvironment.MapPath(
                       "~/Content/Images/Capture/capture.jpg");
                    break;
                case 2:
                    imgPath = HostingEnvironment.MapPath(
                        "~/Content/Images/Upload/upload.jpg");
                    break;
                default:
                    imgPath = HostingEnvironment.MapPath(
                        "~/Content/images/test.png");
                    break;
            }

            try
            {
                if (imgPath != null)
                {
                    IProcessingService service = ServiceFactory.GetProcessingService();
                    ProcessRequestDto request = new ProcessRequestDto
                    {
                        ImagePath = imgPath
                    };

                    ProcessResponseDto response = service.ProcessImage(request);
                    result = response.Controls;
                }

            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return result;
        }
    }
}
