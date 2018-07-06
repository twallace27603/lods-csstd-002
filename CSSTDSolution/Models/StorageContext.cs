using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSSTDEvaluation;
using CSSTDModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CSSTDSolution.Models
{
    public class StorageContext : IStorageContext
    {
        private CloudBlobClient client;
        public StorageContext(string connectionString)
        {
            this.ConnectionString = connectionString;
            var account = CloudStorageAccount.Parse(connectionString);
            client = account.CreateCloudBlobClient();
        }

        public string ConnectionString { get; set; }

        public  List<BlobFileData> GetFileList(string containerName, bool isPrivate)
        {
            var results = new List<BlobFileData>();
            var container = client.GetContainerReference(containerName);
            var sas = isPrivate ? GetSAS(containerName) : "";
            foreach(var blob in container.ListBlobs())
            {
                results.Add(new BlobFileData
                {
                    Name = blob.StorageUri.PrimaryUri.ToString(),
                    URL = blob.StorageUri.PrimaryUri.AbsoluteUri + sas
                });
            }
            return results;
        }

        public  string GetSAS(string containerName)
        {
            var container = client.GetContainerReference(containerName);
            container.CreateIfNotExists();
            return container.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddDays(1)
            });
        }

        public  void UploadFile(string containerName, BlobFileData fileData, bool isPrivate)
        {
            var container = client.GetContainerReference(containerName);
            if (!container.Exists())
            {
                var access = isPrivate ? BlobContainerPublicAccessType.Off : BlobContainerPublicAccessType.Blob;
                container.Create();
                var permissions = new BlobContainerPermissions
                {
                    PublicAccess = access
                };
                container.SetPermissions(permissions);
            }
            var blob = container.GetBlockBlobReference(fileData.Name);
            blob.UploadFromByteArray(fileData.Contents, 0, fileData.Contents.Length);

        }
    }
}