using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSTDModels
{
    public class ProductMention
    {
        public ProductMention()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string Product { get; set; }
        public string Platform { get; set; }
        public DateTime MentionedAt { get; set; }
        public string Mention { get; set; }
    }
}
