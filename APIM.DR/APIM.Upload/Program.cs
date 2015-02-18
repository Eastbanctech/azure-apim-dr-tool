using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIM.Upload
{

    class Program
    {
        static void Main(string[] args)
        {

            CloudBlockBlob blockBlob = APIM.Storage.StorageManager.GetBlobStorage(APIM.Config.Config.BackupName);

            using (var fileStream = System.IO.File.OpenRead(APIM.Config.Config.BackupName))
            {
                blockBlob.UploadFromStream(fileStream);
            } 
        }
    }
}
