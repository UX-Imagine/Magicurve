using System.Configuration;

namespace Uximagine.Magicurve.UI.Web.Common
{
    /// <summary>
    /// The configuration data for web layer.
    /// </summary>
    public class ConfigurationData
    {
        /// <summary>
        /// Gets the name of the download file.
        /// </summary>
        /// <value>
        /// The name of the download file.
        /// </value>
        internal static string DownloadFileName
        {
            get
            {
                return "source.html";
            }
        }

        /// <summary>
        /// Gets the name of the upload file.
        /// </summary>
        /// <value>
        /// The name of the upload file.
        /// </value>
        internal static string UploadFileName
        {
            get
            {
                return "upload.jpg";
            }
        }

        /// <summary>
        /// Gets the index of the upload file.
        /// </summary>
        /// <value>
        /// The index of the upload file.
        /// </value>
        internal static string UploadFileIndex
        {
            get
            {
                return "UploadedImage";
            }
        }

        /// <summary>
        /// Gets the upload directory.
        /// </summary>
        /// <value>
        /// The upload directory.
        /// </value>
        public static string UploadDirectory
        {
            get
            {
                string path = ConfigurationManager.AppSettings["uploadPath"];
                return path;
            }
        }

        /// <summary>
        /// Gets the download directory.
        /// </summary>
        /// <value>
        /// The download directory.
        /// </value>
        public static string DownloadDirectory
        {
            get
            {
                string path = ConfigurationManager.AppSettings["downloadPath"];
                return path;
            }
        }

        /// <summary>
        /// Gets the maximum size of the upload.
        /// </summary>
        /// <value>
        /// The maximum size of the upload.
        /// </value>
        public static int UploadMaxSize
        {
            get
            {
                string value = ConfigurationManager.AppSettings["maxFileSize"];
                int size = int.Parse(value) * 1024 * 1024;
                return size;
            }
        }
    }
}