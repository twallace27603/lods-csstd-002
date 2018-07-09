using CSSTDEvaluation;
using CSSTDModels;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSSTDSolution.Models
{
    public class CosmosDBSQLContext : ICosmosDBSQLContext
    {

        /*
         * 
         *                var CosmosDBKey = CloudConfigurationManager.GetSetting("ListingsKey");
                var CosmosDBUri = CloudConfigurationManager.GetSetting("ListingsURI");
                var databaseName = "realEstate";
                var collectionName = "listings";
                var databaseID = databaseName;
                DocumentClient client = new DocumentClient(new Uri(CosmosDBUri), CosmosDBKey);
                var database = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseID });
                DocumentCollection collectionSpec = new DocumentCollection
                {
                    Id = collectionName
                };
                DocumentCollection collection = await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseID), collectionSpec, new RequestOptions { OfferThroughput = 400  });


                IQueryable<Listing> query = client.CreateDocumentQuery<Listing>(
    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
                if(query.Count<Listing>() == 0)
                {
                    string[] documents = { "https://lodschallenge.blob.core.windows.net/storagechallenges/Listing1.json",
    "https://lodschallenge.blob.core.windows.net/storagechallenges/Listing2.json",
    "https://lodschallenge.blob.core.windows.net/storagechallenges/Listing3.json"};
                    //Load Documents
                    foreach(string documentUri in documents)
                    {
                        await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri("realEstate", "listings"), getListing(documentUri));
                    }

                }


            }
            catch
            {
                result = false;
            }
            return result;

         * */
        private DocumentClient client;
        private string databaseName = "productDB";
        private string collectionName = "products";
        public CosmosDBSQLContext(string connectionString)
        {
            var connect = connectionString.Split(';');
            var uri = connect[0].Split('=')[1];
            var key = connect[1].Split('=')[1] + (connect[1].EndsWith("==") ? "==" : "");

            client = new DocumentClient(new Uri(uri), key);
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public async Task CreateCollection()
        {
            var databaseID = databaseName;
            var database = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseID });
            DocumentCollection collectionSpec = new DocumentCollection
            {
                Id = collectionName
            };
            DocumentCollection collection = await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseID), collectionSpec, new RequestOptions { OfferThroughput = 400 });
        }

        public List<ProductDocument> GetDocuments()
        {
            IQueryable<ProductDocument> query = client.CreateDocumentQuery<ProductDocument>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
            return query.ToList();

        }

        public List<ProductDocument> GetDocuments(string industry)
        {
            IQueryable<ProductDocument> query = client.CreateDocumentQuery<ProductDocument>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName)).Where(p => p.Industry == industry);
            return query.ToList();
        }

        public async Task UploadDocuments(List<ProductDocument> documents)
        {
            await CreateCollection();
            IQueryable<ProductDocument> query = client.CreateDocumentQuery<ProductDocument>(
 UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
            if (query.Count<ProductDocument>() == 0)
            {
                //Load Documents
                foreach (var document in documents)
                {
                    try
                    {
                        var uri = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);
                        var result = await client.UpsertDocumentAsync(uri,document);
                        System.Diagnostics.Trace.WriteLine(result.StatusCode);
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }

        }
    }
}