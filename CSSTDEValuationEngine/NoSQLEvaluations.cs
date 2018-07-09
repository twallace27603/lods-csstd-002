using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSSTDModels;

namespace CSSTDEvaluation
{
    public class NoSQLEvaluationProcessor
    {
        private SampleData sampleData;
        public NoSQLEvaluationProcessor(string baseFolder, string encryptionKey)
        {
            sampleData = new SampleData(baseFolder);
        }
        public NoSQLEvaluationProcessor() { }

        public EvaluationResult<ProductDocument> CosmosDBSQLUpload(ICosmosDBSQLContext context)
        {
            var result = new EvaluationResult<ProductDocument>();
            var data = sampleData.ProductData();
            try
            {
                context.CreateCollection();
                context.UploadDocuments( data);

            } catch (Exception ex)
            {
                result.Code = 2;
                result.Text = $"There was an error uploading product documents to Cosmos DB: {ex.Message}";
            }

            return result;
        }
        public EvaluationResult<ProductDocument> CosmosDBSQLDownload(ICosmosDBSQLContext context)
        {
            var result = new EvaluationResult<ProductDocument>();
            try
            {
                result.Results = context.GetDocuments();
                result.Code = result.Results.Count > 0 ? 0 : 1;
                result.Text = result.Results.Count > 0 ? "Successfully returned product documents from Cosmos DB" : "Did not return any documents from Cosmos DB";

            }
            catch (Exception ex)
            {
                result.Code = 2;
                result.Text = $"There was an error downloading product documents from Cosmos DB: {ex.Message}";
            }
            return result;
        }

        public EvaluationResult<ProductMention> CosmosDBTableUpload(ICosmosDBTableContext context)
        {
            var result = new EvaluationResult<ProductMention>();
            try
            {
                var data = sampleData.ProductMentionData();
                context.CreateTable();
                context.LoadMentions(data);
                result.Code = 0;
                result.Text = "Successfully uploaded table data to Cosmos DB account.";
            }
            catch(Exception ex)
            {
                result.Code = 2;
                result.Text = $"There was an error uploading table data: {ex.Message}";
            }


            return result;
        }

        public EvaluationResult<ProductMention> CosmosDBTableDownload(ICosmosDBTableContext context)
        {
            var result = new EvaluationResult<ProductMention>();
            try
            {
                result.Results = new List<ProductMention>(context.GetMentions());
                result.Code = result.Results.Count > 0 ? 0 : 1;
                result.Text = result.Code == 0 ? "Successfully downloaded table data from Cosmos DB account" :
                    "There were no errors, but no records were returned from Cosmos DB table.";
            }
            catch(Exception ex)
            {
                result.Code = 2;
                result.Text = $"There was an error retrieving tabular data from Cosmos DB: {ex.Message}";
            }

            return result;
        }

    }

}