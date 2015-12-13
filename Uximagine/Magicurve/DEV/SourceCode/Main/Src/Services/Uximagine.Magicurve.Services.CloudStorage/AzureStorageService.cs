namespace Uximagine.Magicurve.Services.CloudStorage
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.File;

    using Uximagine.Magicurve.DataTransfer.Exceptions;

    /// <summary>
    /// The azure storage service.
    /// </summary>
    public class AzureStorageService
    {
        /// <summary>
        /// Gets or sets the FileShare.
        /// </summary>
        /// <value>
        /// The FileShare.
        /// </value>
        public string ShareName { get; set; }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        /// <value>
        /// The directory.
        /// </value>
        public string DirectoryName { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public Uri Uri { get; set; }

        private CloudStorageAccount StorageAccount { get; set; }

        private CloudFileClient FileClient { get; set; }

        private CloudFileShare FileShare { get; set; }

        private CloudFileDirectory FileDirectory { get; set; }

        private CloudFile CloudFile { get; set; }

        public void Upload()
        {
            if (VerifyConfiguration())
            {
                this.BasicAzureFileUploadAsync().Wait();
                this.Uri = this.CloudFile.Uri;
            }
            else
            {
                throw new BusinessException("StorageConnectionString is not valid.");
            }
        }

        public void Download()
        {
            if (VerifyConfiguration())
            {
                this.BasicAzureFileDownloadAsync().Wait();
                this.Uri = this.CloudFile.Uri;
            }
            else
            {
                throw new BusinessException("StorageConnectionString is not valid.");
            }
        }

        /// <summary>
        /// Verifies the configuration.
        /// </summary>
        /// <returns>
        /// <c>true</c> if verified.
        /// </returns>
        private static bool VerifyConfiguration()
        {
            bool configOk = true;
            string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Contains("[AccountName]") || connectionString.Contains("[AccountKey]"))
            {
                configOk = false;
                Debug.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
            }
            return configOk;
        }

        /// <summary>
        /// Basic operations to work with Azure Files
        /// </summary>
        /// <returns>
        /// Task
        /// </returns>
        private async Task BasicAzureFileUploadAsync()
        {
            this.CloudFile = await this.UploadAsync();
        }

        /// <summary>
        /// Basic operations to work with Azure Files
        /// </summary>
        /// <returns>
        /// Task
        /// </returns>
        private async Task BasicAzureFileDownloadAsync()
        {
            this.CloudFile = await this.DownloadAsync();
        }

        private async Task<CloudFile> UploadAsync()
        {
            this.FileShare = await this.GetCloudFileShareAsync();

            this.FileDirectory = await this.GetCloudFileDirectoryAsync();

            // Uploading a local file to the directory created above 
            CloudFile file = this.FileDirectory.GetFileReference(this.FileName);
            await file.UploadFromFileAsync(this.FileName, FileMode.Open);
            return file;
        }

        private async Task<CloudFile> DownloadAsync()
        {
            this.FileShare = await this.GetCloudFileShareAsync();

            this.FileDirectory = await this.GetCloudFileDirectoryAsync();

            CloudFile file = this.FileDirectory.GetFileReference(this.FileName);

            // List all files/directories under the root directory
            List<IListFileItem> results = new List<IListFileItem>();
            FileContinuationToken token = null;
            do
            {
                FileResultSegment resultSegment =
                    await this.FileShare.GetRootDirectoryReference().ListFilesAndDirectoriesSegmentedAsync(token);
                results.AddRange(resultSegment.Results);
                token = resultSegment.ContinuationToken;
            }
            while (token != null);

            // Download the uploaded file to your file system
            await file.DownloadToFileAsync($"./CopyOf{this.FileName}", FileMode.Create);
            return file;
        }

        private async Task<CloudFileDirectory> GetCloudFileDirectoryAsync()
        {
            // Get a reference to the root directory of the FileShare.        
            CloudFileDirectory root = this.FileShare.GetRootDirectoryReference();

            // Create a directory under the root directory 
            CloudFileDirectory dir = root.GetDirectoryReference(this.DirectoryName);
            await dir.CreateIfNotExistsAsync();
            return dir;
        }

        private async Task<CloudFileShare> GetCloudFileShareAsync()
        {
            // Retrieve storage account information from connection string

            this.StorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create a file client for interacting with the file service.

            this.FileClient = this.StorageAccount.CreateCloudFileClient();

            // Create a FileShare for organizing files and directories within the storage account.
            this.FileShare = this.FileClient.GetShareReference(this.ShareName);

            try
            {
                await this.FileShare.CreateIfNotExistsAsync();
            }
            catch (StorageException)
            {
                throw;
            }
            return this.FileShare;
        }
    }
}
