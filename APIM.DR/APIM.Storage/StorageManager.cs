using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIM.Storage
{
    public class StorageManager
    {

        public static CloudBlockBlob GetBlobStorage(string blob, string storageConnectionString = "StorageConnectionString")
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(storageConnectionString));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(APIM.Config.Config.ContainerName);

            return container.GetBlockBlobReference(blob);
        }
    }
}
