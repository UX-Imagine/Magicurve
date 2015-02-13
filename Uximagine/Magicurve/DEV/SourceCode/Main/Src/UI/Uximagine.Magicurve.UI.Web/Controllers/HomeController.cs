#region Imports
using System;
using System.IO;
using System.Web.Mvc; 
#endregion

namespace Uximagine.Magicurve.UI.Web.Controllers
{
    /// <summary>
    /// The Default controller for the web application.
    /// </summary>
    public class HomeController : Controller
    {
        #region Methods - Instance Member - Public Members

        /// <summary>
        /// The Index page of the Web Application.
        /// </summary>
        /// <returns>
        /// The View.
        /// </returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /// <summary>
        /// Samples this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Sample()
        {
            return View();
        }

        /// <summary>
        /// Captures this instance.
        /// </summary>
        public void Capture()
        {
            var stream = Request.InputStream;
            string dump;

            using (var reader = new StreamReader(stream))
            {
                dump = reader.ReadToEnd();
            }

            var path = Server.MapPath("~/Content/Images/Capture/capture.jpg");

            try
            {
                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));
            }
            catch (Exception)
            {
                
                throw;
            }
            
        } 

        #endregion
        
        #region Methods - Instance Member - (helpers)

        /// <summary>
        /// String_s the to_ bytes2.
        /// </summary>
        /// <param name="strInput">
        /// The string input.
        /// </param>
        /// <returns>
        /// The Byte Array.
        /// </returns>
        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];

            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            return bytes;
        } 

        #endregion
    }
}
