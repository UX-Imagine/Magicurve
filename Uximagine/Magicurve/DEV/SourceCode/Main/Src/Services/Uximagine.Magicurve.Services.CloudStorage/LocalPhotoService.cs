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
                fileName = $"task-photo-{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";

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

            return Task.Run(() => { return path; });
        }

        public Task<string> UploadPhotoAsync(Bitmap photoToUpload, string fileName)
        {
            fileName = $"task-photo-{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";

            string path = Path.Combine(HostingEnvironment.MapPath(StorageUtils.UploadDirectory), fileName);

            try
            {
                this.SaveAs(path, photoToUpload.ToStream());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        

            return Task.Run(() => path);
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

            return Task.Run(() => url);
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