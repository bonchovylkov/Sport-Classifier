using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Infrastructure.AzureHelpers
{
    public static class MediaUploader
    {
        public static string UploadImage(Stream content,string picName)
        {

            var storageAccount = CloudStorage.StorageAccount;
            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            var container = blobClient.GetContainerReference("pictures");               

            // Retrieve reference to a blob named "myblob".

            var extention = Path.GetExtension(picName);
            var name = Path.GetFileNameWithoutExtension(picName);
            var guid = Guid.NewGuid().ToString();

            var blockBlob = container.GetBlockBlobReference(string.Format("{0}-{1}{2}",guid,name,extention));
            blockBlob.UploadFromStream(content);
            return blockBlob.Uri.ToString();
        }
    }
}