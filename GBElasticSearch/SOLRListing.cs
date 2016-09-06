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

    public class Fields
    {
        public string content { get; set; }
    }
    public class CloudSearchListing
    {
        public string type { get; set; }
        public CloudSearchListing()
        {
            fields = new Fields();
        }
        public int id { get; set; }
        
        public Fields fields { get; set; }
        

        
        public string content { get; set; }


    }
}