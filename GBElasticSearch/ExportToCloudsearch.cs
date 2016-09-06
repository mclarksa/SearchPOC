using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.CloudSearchDomain;
using Newtonsoft.Json;

using GBDB;
namespace GBElasticSearch
{
    public class ExportToCloudSearch
    {
        public void Export()
        {
            var items = new GbItems().Get().OrderBy(x => x.ItemID);
            var url = "doc-gbtest-voux3h3c56gmmso3rxde47s2mi.us-east-1.cloudsearch.amazonaws.com";
            var client =new Amazon.CloudSearchDomain.AmazonCloudSearchDomainClient(url);
            
            

            int skip = 0;
            int threads = 1;
            var take = 35000;
            var count = items.Count();


            
            while (skip < count)
            {

                var mappeditems = items
                                   .Skip(take / threads)
                                   .Take(take / threads)
                                   .Select(x => new CloudSearchListing
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
                                    id = x.ItemID,
                                    type="add",
                                    content="application/json",
                                    //Manufacturer = x.Char_Manufacturer,
                                    //Model = x.Char_Model,
                                    //ShowcaseListing = x.ShowcaseListing,
                                    //SKU = x.SKU,
                                    //Thumbnail = x.Thumbnail,
                                    fields=new Fields() { content=x.ItemTitle}

                                   })
                                       .ToList();
                var jsonString = JsonConvert.SerializeObject(mappeditems.ToArray());
                var fileSpec = @"c:\temp\l" + skip+ ".json";

                var writer = System.IO.File.CreateText(fileSpec);

                writer.Write(jsonString);
                writer.Flush();
                writer.Close();
                using (WebClient webClient = new WebClient())
                {
                  
                    webClient.Headers.Add("Content-Type:application/json");
                    webClient.UploadFile(url, "POST", fileSpec);
                }

                // var schema = solr.GetSchema();
          
                //solr.AddRange(mappeditems, new AddParameters() { Overwrite = true });
                //solr.Commit();
                skip += take;
                Debug.WriteLine($"Total:{skip}");

            }



        }
    }
}
