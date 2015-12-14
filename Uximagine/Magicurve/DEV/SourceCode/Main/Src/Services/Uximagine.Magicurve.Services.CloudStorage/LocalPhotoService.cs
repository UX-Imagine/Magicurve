namespace Uximagine.Magicurve.Services.CloudStorage
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Security.Policy;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;

    using Uximagine.Magicurve.Core;

    /// <summary>
    ///     The local photo service.
    /// </summary>
    public class LocalPhotoService : IPhotoService
    {
        /// <summary>
        ///     The create and configure asynchronous.
        /// </summary>
        public void CreateAndConfigureAsync()
        {
        }

        /// <summary>
        /// The upload photo asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<string> UploadPhotoAsync(HttpPostedFileBase file, string fileName)
        {
            int maxContentLength = StorageUtils.UploadMaxSize; // Size = 4 MB

            string[] allowedFileExtensions = { ".jpg", ".bmp", ".png", ".jpeg", ".JPG", ".BMP", ".PNG", ".JPEG" };

            string path = string.Empty;

            if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                Debug.WriteLine("File {0}", "Please file of type: " + string.Join(", ", allowedFileExtensions));
            }
            else if (file.ContentLength > maxContentLength)
            {
                Debug.WriteLine(
                    "File {0}", 
                    "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
            }
            else
            {
                path = Path.Combine(HostingEnvironment.MapPath(StorageUtils.UploadDirectory), fileName);

                try
                {
                    this.SaveAs(path, file.InputStream);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            return Task.Run(() => $"{StorageUtils.UploadDirectory}/{fileName}");
        }

        public Task<string> UploadPhotoAsync(Bitmap photoToUpload, string fileName)
        {
           string path = Path.Combine(HostingEnvironment.MapPath(StorageUtils.UploadDirectory), fileName);

            try
            {
                MemoryStream newImageStream = new MemoryStream();
                photoToUpload.Save(newImageStream, System.Drawing.Imaging.ImageFormat.Png);
                //// Reset the Stream to the Beginning before upload
                newImageStream.Seek(0, SeekOrigin.Begin);

                this.SaveAs(path, newImageStream);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        

            return Task.Run(() => $"{StorageUtils.UploadDirectory}/{fileName}");
        }

        /// <summary>
        /// Uploads the text asynchronous.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The source file.
        /// </returns>
        public Task<string> UploadTextAsync(string text, string fileName)
        {
            var path = Path.Combine(HostingEnvironment.MapPath(StorageUtils.UploadDirectory), fileName);

            File.WriteAllText(@path, text);

            string url = path;

            return Task.Run(() => $"{StorageUtils.UploadDirectory}/{fileName}");
        }

        /// <summary>
        /// Loads the image.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <returns>
        /// The image
        /// </returns>
        public Task<Bitmap> LoadImage(string imagePath)
        {
            return Task.Run(() => new Bitmap(imagePath));
        }

        /// <summary>
        ///     Saves as.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="stream">The stream.</param>
        private void SaveAs(string filename, Stream stream)
        {
            using (Bitmap src = Image.FromStream(stream) as Bitmap)
            {
                if (src != null)
                {
                    src.Save(filename);
                    src.Dispose();
                }
            }
        }
    }
}