using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CSSTDModels;

namespace CSSTDModels
{
    public interface ICosmosDBSQLContext
    {
        string ConnectionString { get; set; }
        Task CreateCollection();
        List<ProductDocument> GetDocuments();
        List<ProductDocument> GetDocuments( string industry);
        Task UploadDocuments( List<ProductDocument> documents);
    }
}