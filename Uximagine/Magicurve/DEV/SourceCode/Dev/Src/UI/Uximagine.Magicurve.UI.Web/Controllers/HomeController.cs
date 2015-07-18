#region Imports
using System;
using System.IO;
using System.Web.Mvc;
using System.Linq;
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

            return this.View();
        }

        /// <summary>
        /// Samples this instance.
        /// </summary>
        /// <returns>
        /// The View.
        /// </returns>
        public ActionResult Sample()
        {
            return this.View();
        }

        /// <summary>
        /// Zebras this instance.
        /// </summary>
        /// <returns>
        /// The View.
        /// </returns>
        public ActionResult Zebra()
        {
            return this.View();
        }

        /// <summary>
        /// Templates the matching.
        /// </summary>
        /// <returns>
        /// The view.
        /// </returns>
        public ActionResult Template()
        {
            return this.View();
        }

        /// <summary>
        /// Loading  Canvas Page.
        /// </summary>
        /// <returns>
        /// The View.
        /// </returns>
        public ActionResult Canvas()
        {
            return this.View();
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

            var path = this.Server.MapPath("~/Content/Images/Capture/capture.jpg");

            try
            {
                System.IO.File.WriteAllBytes(path, this.String_To_Bytes2(dump));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Captures the template.
        /// </summary>
        public void CaptureTemplate()
        {
            var stream = Request.InputStream;
            string dump;

            using (var reader = new StreamReader(stream))
            {
                dump = reader.ReadToEnd();
            }

            var path = this.Server.MapPath("~/Content/Images/Capture/template.jpg");

            try
            {
                System.IO.File.WriteAllBytes(path, this.String_To_Bytes2(dump));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Indexes the specified file.
        /// </summary>
        /// <returns>
        /// <c>true</c> if [upload is success].
        /// </returns>
        [HttpPost]
        public ActionResult UploadFile()
        {
            ActionResult result = null;

            if (Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var file = Request.Files["UploadedImage"];

                if (ModelState.IsValid)
                {
                    if (file == null)
                    {
                        ModelState.AddModelError("File", "Please Upload Your file");
                    }
                    else if (file.ContentLength > 0)
                    {
                        const int MaxContentLength = 1024 * 1024 * 4; // Size = 4 MB

                        string[] allowedFileExtensions =
                            {
                                ".jpg", ".bmp", ".png", ".jpeg", ".JPG", ".BMP", ".PNG",
                                ".JPEG"
                            };
                        if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                        {
                            ModelState.AddModelError(
                                "File",
                                "Please file of type: " + string.Join(", ", allowedFileExtensions));
                        }
                        else if (file.ContentLength > MaxContentLength)
                        {
                            ModelState.AddModelError(
                                "File",
                                "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        }
                        else
                        {
                            const string FileName = "upload.jpg";
                            var path = Path.Combine(Server.MapPath("~/Content/Images/Upload"), FileName);
                            file.SaveAs(path);
                            ModelState.Clear();
                            ViewBag.Message = "File uploaded successfully. File path :   ~/Content/Images/Upload/"
                                              + FileName;

                            result = this.Json(new { path = path });

                            return result;
                        }
                    }
                }
            }

            result = this.Json(new { message = "error" });
            return result;
        }

        /// <summary>
        /// Uploads this instance.
        /// </summary>
        /// <returns>
        /// The view.
        /// </returns>
        [HttpGet]
        public ActionResult Upload()
        {
            return this.View();
        }

        /// <summary>
        /// Downloads this instance.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The file
        /// </returns>
        public FileResult Download(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                const string FileName = "source.html";

                var path = Path.Combine(Server.MapPath("~/Content/Files"), FileName);

                System.IO.File.WriteAllText(@path, content);

                byte[] fileBytes = System.IO.File.ReadAllBytes(@path);

                return this.File(fileBytes, System.Net.Mime.MediaTypeNames.Text.Html, FileName); 
            }

            return null;
        }

        /// <summary>
        /// return the navigation
        /// </summary>
        /// <returns> the view </returns>
        public ActionResult _NavBar()
        {
            return this.View("_NavBar");
        }

        /// <summary>
        /// return the menu bar
        /// </summary>
        /// <returns> the view </returns>
        public ActionResult _MenuBar()
        {
            return this.View("_MenuBar");
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
