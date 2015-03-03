using System;
using System.Drawing;
using System.Web.Hosting;
using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.Image.Processing;

namespace Uximagine.Magicurve.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.    
    /// <summary>
    /// The processing Service.
    /// </summary>
    public class ProcessingService : IProcessingService
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The Data.
        /// </returns>
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        /// <summary>
        /// Gets the data using data contract.
        /// </summary>
        /// <param name="composite">
        /// The composite.
        /// </param>
        /// <returns>
        /// The composite type.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// composite exception.
        /// </exception>
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        /// <summary>
        /// Gets the processed image URL.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The saved location.
        /// </returns>
        /// <exception cref="System.Exception">Error processing image  + ex.Message</exception>
        public string GetProcessedImageUrl(ProcessImageRequest request)
        {
            string response = string.Empty;

            Processor processor = new Processor();   
            try
            {
                Bitmap img = processor.ProcessImage(request.ImagePath);
                img.Save(HostingEnvironment.MapPath("~/Content/images/test2.png"));
                img.Dispose();

                response = "~/Content/images/test2.png"; 
            }
            catch (Exception ex)
            {

                throw new Exception("Error processing image " + ex.Message);
            }

            return response;
        }
    }
}
