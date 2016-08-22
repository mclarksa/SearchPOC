using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.JsonNet;
using Newtonsoft.Json;

namespace GBElasticSearch
{
    public sealed class ElasticSearch
    {
        // our instance of ourself as a singleton
        private static readonly ElasticSearch instance = new ElasticSearch();

        ElasticsearchClient client;
        string connectionString = ConfigurationManager.ConnectionStrings["Elasticsearch"].ConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticSearch"/> class.
        /// Follow this: http://nest.azurewebsites.net/elasticsearch-net/connecting.html
        /// We use a ConnectionPool to make the connection fail-over, that means, if the 
        /// connection breaks, it reconnects automatically
        /// </summary>
        private ElasticSearch()
        {
            var node = new Uri(connectionString);
            var connectionPool = new SniffingConnectionPool(new[] { node });
            var config = new ConnectionConfiguration(connectionPool);
            client = new ElasticsearchClient(config);   // exposed in this class
        }

        static ElasticSearch()
        {
        }

        /// <summary>
        /// Gets the instance of our singleton class
        /// </summary>
        /// <value>The instance.</value>
        public static ElasticSearch Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Log the specified module, id and json.
        /// </summary>
        /// <param name="type">Here the entity you want to save your log,
        /// let's use it based on classes and StateMachines</param>
        /// <param name="id">Identifier. alwayes the next</param>
        /// <param name="json">Json.</param>
        public void Log(string type, string id, string json)
        {
            client.Index("mta_log", type, id, json);
        }

    }
    public class ExportToElasticSearch
    {
        public ExportToElasticSearch()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Elasticsearch"];
            var node = new Uri("http://localhost:9200");
            var connectionPool = new Elasticsearch.Net.ConnectionPool.SniffingConnectionPool(new[] {node});
            var config = new ConnectionConfiguration(connectionPool);
            Client = new ElasticsearchClient(config);
        }
        private static ElasticsearchClient Client { get; set; }
        
        public void GetGBData()
        {
            
            var dbitems = new GBDB.GbItems();
            var items =dbitems.Get();
            decimal count = items.LongCount();
            decimal itemCount = 0;
            decimal pct = 0.000000000M;
            foreach (var item in items)
            {
                Client.Index("gunbroker", "listing", JsonConvert.SerializeObject(item));
                itemCount++;
                pct = itemCount/count;
                Debug.WriteLine(pct);
            }
            

        }
    }
}
