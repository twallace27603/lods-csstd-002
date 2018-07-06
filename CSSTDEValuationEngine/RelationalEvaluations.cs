using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSSTDModels;

namespace CSSTDEvaluation
{
    public class RelationalEvaluationProcessor
    {
        private SampleData sampleData;
        public RelationalEvaluationProcessor(string baseFolder)
        {
            sampleData = new SampleData(baseFolder);
        }
        public RelationalEvaluationProcessor() { }
 
        public EvaluationResult<CustomerData> SQLServerUpload(ISQLServerContext context)
        {
            var result = new EvaluationResult<CustomerData>();
            try
            {
                context.CreateTable();
                context.LoadData( sampleData.CustomerData());
                result.Text = "Successfully uploaded customer data to SQL Server";
                result.Code = 0;
            }
            catch (Exception ex)
            {
                result.Code = 2;
                result.Text = $"Error loading customer data to SQL Server: {ex.Message}";
            }
            return result;
        }

        public EvaluationResult<CustomerData> SQLServerDownload(ISQLServerContext context)
        {
            var result = new EvaluationResult<CustomerData>();
            try
            {
                result.Results = context.GetData();
                result.Code = result.Results.Count > 0 ? 0 : 1;
                result.Text = result.Results.Count>0 ? "Successfully downloaded customer data" : "There was no customer data downloaded.";
            }
            catch (Exception ex)
            {
                result.Code = 2;
                result.Text = $"Error loading data: {ex.Message}";
            }
            return result;
        }

        public EvaluationResult<VendorData> MySQLUpload(IMySQLContext context)
        {
            var result = new EvaluationResult<VendorData>();
            try
            {
                context.CreateTable();
                context.LoadData(sampleData.VendorData());
                result.Text = "Successfully uploaded vendor data to MySQL";
                result.Code = 0;
            }
            catch (Exception ex)
            {
                result.Code = 2;
                result.Text = $"Error loading vendor data to MySQL: {ex.Message}";
            }
            return result;
        }

        public EvaluationResult<VendorData> MySQLDownload(IMySQLContext context)
        {
            var result = new EvaluationResult<VendorData>();
            try
            {
                result.Results = context.GetData();
                result.Code = result.Results.Count > 0 ? 0 : 1;
                result.Text = result.Results.Count > 0 ? "Successfully downloaded vendor data" : "There was no vendor data downloaded.";
            }
            catch (Exception ex)
            {
                result.Code = 2;
                result.Text = $"Error loading data: {ex.Message}";
            }
            return result;
        }
    }

}