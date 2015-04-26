#region Imports
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Http;
using Uximagine.Magicurve.DataTransfer.Responses;
using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.Services;
using Uximagine.Magicurve.Services.Client;
#endregion
namespace Uximagine.Magicurve.UI.Web.Controllers
{
   

    /// <summary>
    /// The API controller.
    /// </summary>
    public class ValuesController : ApiController
    {
        /// <summary>
        /// GET API/values
        /// </summary>
        /// <returns>
        /// The list.
        /// </returns>
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets the edges.
        /// </summary>
        /// <returns>
        /// The edge image.
        /// </returns>
        [HttpGet]
        public string Edges()
        {
            const string SavedPath = "/Content/images/test2.png";
            string imgPath = HostingEnvironment.MapPath(string.Empty) +
               "\\Content\\Images\\Capture\\capture.jpg";

            IProcessingService service = new ProcessigService();
            ProcessRequestDto request = new ProcessRequestDto { ImagePath = imgPath };
            ProcessResponseDto response = service.GetEdgeProcessedImageUrl(request);

            if (response == null || response.Image == null)
            {
                return SavedPath;
            }

            response.Image.Save(HostingEnvironment.MapPath("~/Content/images/test2.png"));
            response.Image.Dispose();

            return SavedPath;
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
        public string Get(int id)
        {
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
                    IProcessingService service = new ProcessigService();
                    ProcessRequestDto request = new ProcessRequestDto();
                    request.ImagePath = imgPath;
                    ProcessResponseDto response = service.GetEdgeProcessedImageUrl(request);
                    if (response != null && response.Image != null)
                    {
                        response.Image.Save(HostingEnvironment.MapPath("~/Content/images/test2.png"));
                        response.Image.Dispose();
                    }

                }

            }
            catch (System.Exception)
            {

            }

            return "/Content/images/test2.png";
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Puts the specified identifier.
        /// API/values/5
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// // DELETE API/values/5    
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        public void Delete(int id)
        {
        }
    }
}
