using Uximagine.Magicurve.DataTransfer.Responses;
using Uximagine.Magicurve.DataTransfer.Requests;

namespace Uximagine.Magicurve.Services
{
    using System.Drawing;
    using System.Threading.Tasks;

    /// <summary>
    /// The service to handle file save update or delete.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// Id of the file.
        /// </returns>
        Task<FileSaveResponse> SaveFile(FileSaveRequest request);

        /// <summary>
        /// The save file.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<FileSaveResponse> SaveFile(string text, string fileName);

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The URL.
        /// </returns>
        Task<FileSaveResponse> SaveFile(Bitmap image, string fileName);

        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// <c>true</c> if success otherwise <c>false</c>
        /// </returns>
        FileUpdateResponse UpdateFile(FileUpdateRequest request);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// <c>true</c> if success otherwise <c>false</c>
        /// </returns>
        FileDeleteResponse DeleteFile(FileDeleteRequest request);

        /// <summary>
        /// The load file.
        /// </summary>
        /// <param name="imagePath">
        /// The image path.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<Bitmap> LoadImageFile(string imagePath);
    }
}