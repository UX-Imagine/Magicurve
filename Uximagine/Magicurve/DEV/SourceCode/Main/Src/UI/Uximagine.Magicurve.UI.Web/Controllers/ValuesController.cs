#region Imports
using System.Collections.Generic;
using System.Drawing;
using System.Web.Hosting;
using System.Web.Http;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Image.Processing;

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
            return new string[] { "value1", "value2" };
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
            Processor processor = new Processor();
            string imgPath;

            switch (id)
            {
                case 1:
                    imgPath = HostingEnvironment.MapPath(
                       "~/Content/Images/Capture/capture.jpg");
                    break;
                case 2:
                    imgPath = HostingEnvironment.MapPath(
                        "~/Content/images/tri.png");
                    break;
                default:
                    imgPath = HostingEnvironment.MapPath(
                        "~/Content/images/test.png");
                    break;
            }

            try
            {
                Bitmap img = processor.ProcessImage(imgPath);
                img.Save(HostingEnvironment.MapPath("~/Content/images/test2.png"));
                img.Dispose(); 
            }
            catch (System.Exception)
            {
                
                throw;
            }
           
            IControl shape = processor.Shape;

            if (id.Equals(2))
            {
                return shape.Type.ToString();
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
