using SolrNet.Attributes;

namespace GBElasticSearch
{
    public class SOLRListing
    {
        [SolrUniqueKey("id")]
        public int Id { get; set; }

        [SolrField("item_id")]
        public int ItemId{get;set;}

        [SolrField("content")]
        public string Title { get; set; }
        

    }
}