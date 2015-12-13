using System.Threading.Tasks;
using System.Web;

namespace Uximagine.Magicurve.Services
{
    using System.Drawing;

    /// <summary>
    /// The PhotoService interface.
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// Creates the and configure asynchronous.
        /// </summary>
        void CreateAndConfigureAsync();

        /// <summary>
        /// Uploads the photo asynchronous.
        /// </summary>
        /// <param name="photoToUpload">The photo to upload.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The saved file information.
        /// </returns>
        Task<string> UploadPhotoAsync(HttpPostedFileBase photoToUpload, string fileName);

        /// <summary>
        /// Uploads the photo asynchronous.
        /// </summary>
        /// <param name="photoToUpload">The photo to upload.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The url.
        /// </returns>
        Task<string> UploadPhotoAsync(Bitmap photoToUpload, string fileName);

        /// <summary>
        /// The upload text asynchronous.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<string> UploadTextAsync(string text, string fileName);

        /// <summary>
        /// The load image.
        /// </summary>
        /// <param name="imagePath">
        /// The image path.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<Bitmap> LoadImage(string imagePath);
    }
}

