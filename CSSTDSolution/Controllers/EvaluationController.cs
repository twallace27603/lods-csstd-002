﻿using System.Web.Http;
using CSSTDSolution.Models;
using CSSTDModels;
using CSSTDEvaluation;
using System.Configuration;

namespace CSSTDSolution.Controllers
{
    [RoutePrefix("evaluate")]
    public class EvaluationController : ApiController
    {
        private string baseFolder = System.Web.HttpContext.Current.Server.MapPath("~/SampleData/");

        [Route("publicupload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> PublicBlobUploadTest(string storageAccount, string storageKey, string encryptionKey)
        {
            storageAccount = storageAccount == "-1" ? ConfigurationManager.AppSettings["storageAccount"] : storageAccount;
            storageKey = storageKey == "-1" ? ConfigurationManager.AppSettings["storageKey"] : storageKey;
            return new BlobEvaluationProcessor(baseFolder, encryptionKey).PublicBlobUpload(new StorageContext(storageAccount, storageKey));
        }

        [Route("publicdownload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> PublicBlobDownloadTest(string storageAccount, string storageKey, string encryptionKey)
        {
            storageAccount = storageAccount == "-1" ? ConfigurationManager.AppSettings["storageAccount"] : storageAccount;
            storageKey = storageKey == "-1" ? ConfigurationManager.AppSettings["storageKey"] : storageKey;
            return new BlobEvaluationProcessor(baseFolder, encryptionKey).PublicBlobDownload(new StorageContext(storageAccount, storageKey));
        }

        [Route("privateupload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> PrivateBlobUploadTest(string storageAccount, string storageKey, string encryptionKey)
        {
            storageAccount = storageAccount == "-1" ? ConfigurationManager.AppSettings["storageAccount"] : storageAccount;
            storageKey = storageKey == "-1" ? ConfigurationManager.AppSettings["storageKey"] : storageKey;
            return new BlobEvaluationProcessor(baseFolder, encryptionKey).PrivateBlobUpload(new StorageContext(storageAccount, storageKey));
        }

        [Route("privatedownload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> PrivateBlobDownloadTest(string storageAccount, string storageKey, string encryptionKey)
        {
            storageAccount = storageAccount == "-1" ? ConfigurationManager.AppSettings["storageAccount"] : storageAccount;
            storageKey = storageKey == "-1" ? ConfigurationManager.AppSettings["storageKey"] : storageKey;
            return new BlobEvaluationProcessor(baseFolder, encryptionKey).PrivateBlobDownload(new StorageContext(storageAccount, storageKey));

        }

        [Route("privatesas")]
        [HttpGet]
        public EvaluationResult<string> PrivateBlobSASTest(string storageAccount, string storageKey, string encryptionKey)
        {
            storageAccount = storageAccount == "-1" ? ConfigurationManager.AppSettings["storageAccount"] : storageAccount;
            storageKey = storageKey == "-1" ? ConfigurationManager.AppSettings["storageKey"] : storageKey;
            return new BlobEvaluationProcessor(baseFolder, encryptionKey).PrivateContainerSAS(new StorageContext(storageAccount, storageKey));
        }

        [Route("sqlupload")]
        [HttpGet]
        public EvaluationResult<CustomerData> SQLServerUploadTest(string connectionString, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["SQLConnection"] : connectionString;
            return new RelationalEvaluationProcessor(baseFolder, encryptionKey).SQLServerUpload(new SQLServerContext(connectionString));
        }

        [Route("sqldownload")]
        [HttpGet]
        public EvaluationResult<CustomerData> SQLServerDownloadTest(string connectionString, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["SQLConnection"] : connectionString;
            return new RelationalEvaluationProcessor(baseFolder, encryptionKey).SQLServerDownload(new SQLServerContext(connectionString));
        }

        [Route("mysqlupload")]
        [HttpGet]
        public EvaluationResult<VendorData> MySQLUploadTest(string connectionString, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["MySQLConnection"] : connectionString;
            return new RelationalEvaluationProcessor(baseFolder, encryptionKey).MySQLUpload(new MySQLContext(connectionString));
        }

        [Route("mysqldownload")]
        [HttpGet]
        public EvaluationResult<VendorData> MySQLDownloadTest(string connectionString, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["MySQLConnection"] : connectionString;
            return new RelationalEvaluationProcessor(baseFolder, encryptionKey).MySQLDownload(new MySQLContext(connectionString));
        }

        [Route("cosmossqlupload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> CosmosDBSQLUploadTest(string connectionString, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["CosmosDBSQLConnection"] : connectionString;
            return new NoSQLEvaluationProcessor(baseFolder, encryptionKey).CosmosDBSQLUpload(new CosmosDBSQLContext(connectionString));
        }

        [Route("cosmossqldownload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> CosmosDBSQLDownloadTest(string connectionString, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["CosmosDBSQLConnection"] : connectionString;
            return new NoSQLEvaluationProcessor(baseFolder, encryptionKey).CosmosDBSQLDownload(new CosmosDBSQLContext(connectionString));
        }

        [Route("cosmostableupload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> CosmosDBTableUploadTest(string connectionString, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["CosmosDBTableConnection"] : connectionString;
            return new NoSQLEvaluationProcessor(baseFolder, encryptionKey).CosmosDBSQLUpload(new CosmosDBSQLContext(connectionString));
        }

        [Route("cosmostabledownload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> CosmosDBTableDownloadTest(string connectionString, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["CosmosDBTableConnection"] : connectionString;
            return new NoSQLEvaluationProcessor(baseFolder, encryptionKey).CosmosDBSQLDownload(new CosmosDBSQLContext(connectionString));
        }

    }
}
