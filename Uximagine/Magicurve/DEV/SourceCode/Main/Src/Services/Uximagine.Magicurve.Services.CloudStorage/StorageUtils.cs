namespace Uximagine.Magicurve.Services.CloudStorage
{
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;

    static class StorageUtils
    {
        public static CloudStorageAccount StorageAccount
        {
            get
            {
                string account = CloudConfigurationManager.GetSetting("StorageAccountName");
                // This enables the storage emulator when running locally using the Azure compute emulator.
                if (account == "{StorageAccountName}")
                {
                    return CloudStorageAccount.DevelopmentStorageAccount;
                }

                string key = CloudConfigurationManager.GetSetting("StorageAccountAccessKey");
                string connectionString = $"DefaultEndpointsProtocol=https;AccountName={account};AccountKey={key}";
                return CloudStorageAccount.Parse(connectionString);
            }
        }

        public static int UploadMaxSize
        {
            get
            {
                return 4 * 1024 * 1024;
            }
        }

        public static string UploadFileName
        {
            get
            {
                return "upload.jpg";
            } }

        public static string UploadDirectory
        {
            get
            {
                return "~/Content/Images/Upload/";
            }
        }
    }
}
