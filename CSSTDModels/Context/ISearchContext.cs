using CSSTDModels;
using System.Collections.Generic;

namespace CSSTDModels
{
    public interface ISearchContext
    {
        string ConnectionString { get; set; }
        void CreateIndex();
        List<ProductDocument> GetDocuments(string Industry);


    }

}