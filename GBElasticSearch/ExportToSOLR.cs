using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.DSL;
using SolrNet.Exceptions;
using GBDB;
namespace GBElasticSearch
{
    public class ExportToSOLR
    {
        public void Export()
        {
            var items = new GbItems().Get().OrderBy(x=>x.ItemID);
            
            Startup.Init<SOLRListing>(@"http://localhost:8983/solr/SOLRListing");

            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SOLRListing>>();


            int skip = 0;
            int threads = 1;
            var take = 1000;
            var count = items.Count();
            



            while (skip < count)
            {

             var mappeditems = items
                                .Skip(take / threads)
                                .Take(take / threads)
                                .Select(x => new SOLRListing
                                    {
                                    //    BarrelLength = x.Char_BarrelLength,
                                    //    BidCount = x.BidCount,
                                    //    Caliber = x.Char_Caliber,
                                    //    CategoryID = x.CategoryID,
                                    //    EndingDate = x.EndingDate,
                                    //    StartingDate = x.StartingDate,
                                    //    FeaturedListing = x.FeaturedListing,
                                    //    FrameFinish = x.Char_FrameFinish,
                                    //    SlideFinish = x.Char_SlideFinish,
                                    //    Gauge = x.Char_Gauge,
                                        Id = x.ItemID,
                                        //Manufacturer = x.Char_Manufacturer,
                                        //Model = x.Char_Model,
                                        //ShowcaseListing = x.ShowcaseListing,
                                        //SKU = x.SKU,
                                        //Thumbnail = x.Thumbnail,
                                        Title = x.ItemTitle

                                    })
                                    .ToList();
                //var schema = solr.GetSchema();
                solr.AddRange(mappeditems, new AddParameters() { Overwrite = true });
                solr.Commit();
                skip += take;
                Debug.WriteLine($"Total:{skip}");

            }



        }
    }
}
