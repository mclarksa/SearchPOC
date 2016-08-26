using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using GBDB;
using Nest;
using Newtonsoft.Json;

namespace GBElasticSearch
{
    public sealed class ElasticSearch
    {
        // our instance of ourself as a singleton
        private static readonly ElasticSearch instance = new ElasticSearch();

        ElasticClient client;
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
            var config = new ConnectionSettings(connectionPool);

            client = new Nest.ElasticClient(config);


        }

        static ElasticSearch()
        {
        }

        /// <summary>
        /// Gets the instance of our singleton class
        /// </summary>
        /// <value>The instance.</value>
        public static ElasticSearch Instance => instance;

        /// <summary>
        /// Log the specified module, id and json.
        /// </summary>
        /// <param name="type">Here the entity you want to save your log,
        /// let's use it based on classes and StateMachines</param>
        /// <param name="id">Identifier. alwayes the next</param>
        /// <param name="json">Json.</param>
        public void Log(string type, string id, string json)
        {
            // client.Index("listing","mta_log", type, id, json);
        }

    }
    public class ExportToElasticSearch
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Elasticsearch"].ConnectionString;
        public ExportToElasticSearch()
        {
            var node = new Uri(connectionString);
            var connectionPool = new SniffingConnectionPool(new[] { node });
            connectionPool.SniffedOnStartup = true;
            
            var config = new ConnectionSettings(connectionPool);
            
            Client = new Nest.ElasticClient(config);
        }
        private static Nest.ElasticClient Client { get; set; }

        public void TransferDbListings()
        {

            var dbitems = new GBDB.GbItems();
            var items = dbitems.Get();

            
            int skip = 0;
            int threads = 5;
            var take = 1000;
            var count = items.Count();
            var node = new Uri(connectionString);
            


            while (skip < count)
            {



                var mydict = new Dictionary<string, string>();

                var dee = items.OrderBy(x => x.ItemID).Skip(skip).Take(take).Select(item => item).ToArray();
                var request = new List<List<Item>>();
                for (int i = 0; i < threads; i++)
                {
                    request.Add(
                        new List<Item>(dee.Skip(i * take/threads).Take(take/threads).Select(x => x).ToList()));
                }
              
                Parallel.ForEach<List<Item>>(request, p =>
                {

                    var connectionPool = new SniffingConnectionPool(new[] { node });
                    connectionPool.SniffedOnStartup = true;


                    var config = new ConnectionSettings(connectionPool);
                    var client = new Nest.ElasticClient(config);


                    client.IndexMany<Item>(p, "gunbroker", "listing");
                });



                skip += take;


            }



        }
    }
}
