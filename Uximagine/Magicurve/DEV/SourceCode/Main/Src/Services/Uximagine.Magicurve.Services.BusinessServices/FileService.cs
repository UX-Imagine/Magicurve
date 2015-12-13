using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.DataTransfer.Responses;

namespace Uximagine.Magicurve.Services.BusinessServices
{
    using System.Drawing;
    using System.Threading.Tasks;

    using Uximagine.Magicurve.Services.CloudStorage;

    /// <summary>
    /// The file handling service.
    /// </summary>
    public class FileService : BusinessService, IFileService
    {
        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The URI.
        /// </returns>
        public async Task<FileSaveResponse> SaveFile(FileSaveRequest request)
        {
            FileSaveResponse response = new FileSaveResponse();
            IPhotoService service = ServiceFactory.GetPhotoService();
            service.CreateAndConfigureAsync();
            response.Uri = await service.UploadPhotoAsync(request.Image, request.Name);
            return response;
        }

        /// <summary>
        /// The save file.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileSaveResponse> SaveFile(Bitmap image, string fileName)
        {
            FileSaveResponse response = new FileSaveResponse();
            IPhotoService service = ServiceFactory.GetPhotoService();
            service.CreateAndConfigureAsync();
            response.Uri = await service.UploadPhotoAsync(image, fileName);
            return response;
        }

        /// <summary>
        /// The save file.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<FileSaveResponse> SaveFile(string text, string fileName)
        {
            FileSaveResponse response = new FileSaveResponse();
            IPhotoService service = ServiceFactory.GetPhotoService();
            service.CreateAndConfigureAsync();
            response.Uri = await service.UploadTextAsync(text, fileName);
            return response;
        }



        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// update is success.
        /// </returns>
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

        public Task<Bitmap> LoadImageFile(string imagePath)
        {
            IPhotoService photoService = ServiceFactory.GetPhotoService();
            return photoService.LoadImage(imagePath);

        }
    }
}