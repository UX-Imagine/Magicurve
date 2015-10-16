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
        public List<Control> GetControls()
        {
            //// const string SavedPath = "/Content/images/test2.png";
            string imgPath = HostingEnvironment.MapPath(string.Empty) +
               "\\Content\\Images\\Capture\\capture.jpg";

            IProcessingService service = ServiceFactory.GetProcessingService();
            ProcessRequestDto request = new ProcessRequestDto { ImagePath = imgPath };
            ProcessResponseDto response = service.ProcessImage(request);

            var json = response.Controls;
            response.ImageResult.Save(HostingEnvironment.MapPath("~/Content/images/test2.png"));
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

            return this.Json(new
                                {
                                    url = "/Content/images/result.png",
                                    controls = response.Controls
                                });
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
