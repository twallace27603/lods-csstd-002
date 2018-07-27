using CSSTDModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;

namespace CSSTDSolution.Models
{
    public class StorageContext : IStorageContext
    {
        private CloudBlobClient client;
        public StorageContext(string storageAccount, string storageKey)
        {
            var account = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(storageAccount, storageKey), true);
            client = account.CreateCloudBlobClient();
        }

        public string ConnectionString { get; set; }

        public List<BlobFileData> GetFileList(string containerName, bool isPrivate)
        {
            var results = new List<BlobFileData>();
            var container = client.GetContainerReference(containerName);
            var sas = isPrivate ? GetSAS(containerName) : "";
            foreach (var blob in container.ListBlobs())
            {
                results.Add(new BlobFileData
                {
                    Name = blob.StorageUri.PrimaryUri.ToString(),
                    URL = blob.StorageUri.PrimaryUri.AbsoluteUri,
                    SAS = sas
                });
            }
            return results;
        }

        public string GetSAS(string containerName)
        {
            var container = client.GetContainerReference(containerName);
            container.CreateIfNotExists();
            return container.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddDays(1)
            });
        }

        public void UploadFile(string containerName, BlobFileData fileData, bool isPrivate)
        {
            var container = client.GetContainerReference(containerName);
            if (!container.Exists())
            {
                container.Create();
                var permissions = new BlobContainerPermissions
                {
                    PublicAccess = isPrivate ? BlobContainerPublicAccessType.Off : BlobContainerPublicAccessType.Blob
                };
                container.SetPermissions(permissions);
            }
            var blob = container.GetBlockBlobReference(fileData.Name);
            blob.UploadFromByteArray(fileData.Contents, 0, fileData.Contents.Length);

        }
    }
}