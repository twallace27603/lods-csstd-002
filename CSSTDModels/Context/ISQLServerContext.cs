using CSSTDModels;
using System.Collections.Generic;

namespace CSSTDModels
{
    public interface ISQLServerContext
    {
        string ConnectionString { get; set; }
        void CreateTable();
        void LoadData(List<CustomerData> customers);
        List<CustomerData> GetData();

    }

}