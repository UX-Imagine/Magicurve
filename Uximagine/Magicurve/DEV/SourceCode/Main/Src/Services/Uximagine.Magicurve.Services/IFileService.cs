using Uximagine.Magicurve.DataTransfer.Responses;
using Uximagine.Magicurve.DataTransfer.Requests;

namespace Uximagine.Magicurve.Services
{
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
        FileSaveResponse SaveFile(FileSaveRequest request);

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
    }
}