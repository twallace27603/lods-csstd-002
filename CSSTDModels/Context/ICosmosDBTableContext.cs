using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSTDModels
{
    public interface ICosmosDBTableContext
    {
        string ConnectionString { get; set; }
        Task CreateTable();
        List<ProductMention> GetMentions();
        List<ProductMention> GetMentions(string product, string platform);
        Task LoadMentions(List<ProductMention> mentions);

    }
}
