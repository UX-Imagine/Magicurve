using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.DataTransfer.Responses;

namespace Uximagine.Magicurve.Services.BusinessServices
{
    /// <summary>
    /// The file handling servie.
    /// </summary>
    public class FileService : BusinessService, IFileService
    {
        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public FileSaveResponse SaveFile(FileSaveRequest request)
        {
            FileSaveResponse response = null;

            return response;
        }

        public FileUpdateResponse UpdateFile(FileUpdateRequest request)
        {
            FileUpdateResponse response = null;

            return response;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// <c>true</c> if success otherwise <c>fasle</c>
        /// </returns>
        public FileDeleteResponse DeleteFile(FileDeleteRequest request)
        {
            FileDeleteResponse response = null;

            return response;
        }
    }
}