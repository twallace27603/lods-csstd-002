using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSSTDModels;

namespace CSSTDModels
{
    public interface IStorageContext
    {
        string ConnectionString { get; set; }
        List<BlobFileData> GetFileList( string containerName, bool isPrivate);
        void UploadFile( string containerName, BlobFileData fileData, bool isPrivate);
        string GetSAS( string containerName);

    }

}