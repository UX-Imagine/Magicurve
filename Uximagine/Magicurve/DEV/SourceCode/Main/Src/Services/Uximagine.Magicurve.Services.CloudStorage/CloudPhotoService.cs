namespace Uximagine.Magicurve.Services.CloudStorage
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using Uximagine.Magicurve.Core;
    using Uximagine.Magicurve.Services;

    /// <summary>
    /// The photo service.
    /// </summary>
    public class CloudPhotoService : IPhotoService
    {
        /// <summary>
        /// The create and configure asynchronous.
        /// </summary>
        public async void CreateAndConfigureAsync()
        {
            try
            {
                CloudStorageAccount storageAccount = StorageUtils.StorageAccount;

                // Create a blob client and retrieve reference to images container
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                // Create the "images" container if it doesn't already exist.
                if (await container.CreateIfNotExistsAsync())
                {
                    // Enable public access on the newly created "images" container
                    await container.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess =
                                BlobContainerPublicAccessType.Blob
                        });

                    //// log.Information("Successfully created Blob Storage Images Container and made it public");
                }
            }
            catch (Exception ex)
            {
                //// .Error(ex, "Failure to Create or Configure images container in Blob Storage Service");
                throw;
            }
        }

        /// <summary>
        /// The upload photo asynchronous.
        /// </summary>
        /// <param name="photoToUpload">The photo to upload.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<string> UploadPhotoAsync(HttpPostedFileBase photoToUpload, string fileName)
        {
            if (photoToUpload == null || photoToUpload.ContentLength == 0)
            {
                return null;
            }

            string fullPath = null;
            Stopwatch timespan = Stopwatch.StartNew();

            try
            {
                CloudStorageAccount storageAccount = StorageUtils.StorageAccount;

                // Create the blob client and reference the container
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                // Create a unique name for the images we are about to upload
                string imageName = string.Format("task-photo-{0}{1}",
                    Guid.NewGuid().ToString(),
                    Path.GetExtension(photoToUpload.FileName));

                // Upload image to Blob Storage
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
                blockBlob.Properties.ContentType = photoToUpload.ContentType;
                await blockBlob.UploadFromStreamAsync(photoToUpload.InputStream);

                fullPath = blockBlob.Uri.ToString();

                timespan.Stop();
                //// log.TraceApi("Blob Service", "CloudPhotoService.UploadPhoto", timespan.Elapsed, "imagepath={0}", fullPath);
            }
            catch (Exception ex)
            {
                //// log.Error(ex, "Error upload photo blob to storage");
                throw;
            }

            return fullPath;
        }

        public async Task<string> UploadPhotoAsync(Bitmap photoToUpload, string fileName)
        {
            if (photoToUpload == null)
            {
                return null;
            }

            string fullPath = null;
            Stopwatch timespan = Stopwatch.StartNew();

            try
            {
                CloudStorageAccount storageAccount = StorageUtils.StorageAccount;

                // Create the blob client and reference the container
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                // Create a unique name for the images we are about to upload
                string imageName = $"task-photo-{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";

                // Upload image to Blob Storage
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);

                MemoryStream newImageStream = new MemoryStream();
                photoToUpload.Save(newImageStream, System.Drawing.Imaging.ImageFormat.Png);

                // Reset the Stream to the Beginning before upload
                newImageStream.Seek(0, SeekOrigin.Begin);

                blockBlob.Properties.ContentType = "image/png";
                
                await blockBlob.UploadFromStreamAsync(newImageStream);

                fullPath = blockBlob.Uri.ToString();

                timespan.Stop();
                //// log.TraceApi("Blob Service", "CloudPhotoService.UploadPhoto", timespan.Elapsed, "imagepath={0}", fullPath);
            }
            catch (Exception ex)
            {
                //// log.Error(ex, "Error upload photo blob to storage");
                throw;
            }

            return fullPath;
        }

        public async Task<string> UploadTextAsync(string text, string fileName)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            string fullPath = null;
            Stopwatch timespan = Stopwatch.StartNew();

            try
            {
                CloudStorageAccount storageAccount = StorageUtils.StorageAccount;

                // Create the blob client and reference the container
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("texts");

                // Create a unique name for the images we are about to upload
                string imageName = $"take-text-{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";

                // Upload image to Blob Storage
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
                blockBlob.Properties.ContentType = "text/html";

                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(System.Text.Encoding.UTF8.GetBytes(text), 0, System.Text.Encoding.UTF8.GetBytes(text).Length);
                    memoryStream.Position = 0;
                    await blockBlob.UploadFromStreamAsync(memoryStream);
                }

                fullPath = blockBlob.Uri.ToString();

                timespan.Stop();
                //// log.TraceApi("Blob Service", "CloudPhotoService.UploadPhoto", timespan.Elapsed, "imagepath={0}", fullPath);
            }
            catch (Exception ex)
            {
                //// log.Error(ex, "Error upload photo blob to storage");
                throw;
            }

            return fullPath;
        }

        /// <summary>
        /// Loads the image.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <returns>
        /// The image.
        /// </returns>
        public async Task<Bitmap> LoadImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return null;
            }

            string fullPath = null;
            Stopwatch timespan = Stopwatch.StartNew();

            Bitmap image;
            try
            {
                CloudStorageAccount storageAccount = StorageUtils.StorageAccount;

                // Create the blob client and reference the container
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Create a unique name for the images we are about to upload

                // Upload image to Blob Storage
                CloudBlockBlob blockBlob = new CloudBlockBlob(new Uri(imagePath), storageAccount.Credentials);

                using (var memoryStream = new MemoryStream())
                {
                    await blockBlob.DownloadToStreamAsync(memoryStream);
                    image = new Bitmap(memoryStream);
                }


                timespan.Stop();
            }
            catch (Exception ex)
            {
                //// log.Error(ex, "Error upload photo blob to storage");
                throw;
            }

            return image;
        }
    }
}
