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
        public NoSQLEvaluationProcessor(string baseFolder)
        {
            sampleData = new SampleData(baseFolder);
        }
        public NoSQLEvaluationProcessor() { }

        public EvaluationResult<ProductDocument> CosmosDBUpload(ICosmosDBContext context)
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
        public EvaluationResult<ProductDocument> CosmosDBDownload(ICosmosDBContext context)
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
        public EvaluationResult<ProductDocument> SearchIndex(ISearchContext context)
        {
            var result = new EvaluationResult<ProductDocument>();
            try
            {
                context.CreateIndex();
                result.Text = "Successfully created document search engine";
                result.Code = 0;
            }
            catch (Exception ex)
            {
                result.Code = 2;
                result.Text = $"There was an error building the product document search index: {ex.Message}";
            }
            return result;
        }

        public EvaluationResult<ProductDocument> SearchDownload(ISearchContext context)
        {
            var result = new EvaluationResult<ProductDocument>();
            try
            {
                result.Results = context.GetDocuments( "swimming");
                result.Text = result.Results.Count>0 ? "Successfully performed search on product documents" : "No product documents were returned by the search.";
                result.Code = 0;

            }
            catch (Exception ex)
            {
                result.Code = 2;
                result.Text = $"There was an error downloading product document search results: {ex.Message}";
            }
            return result;
        }


    }

}