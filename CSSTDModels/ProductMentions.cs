using System;

namespace CSSTDModels
{
    public class ProductMention
    {

        string ID { get; set; }
        string Product { get; set; }
        string Platform { get; set; }
        DateTime MentionedAt { get; set; }
        string Mention { get; set; }
    }
}
