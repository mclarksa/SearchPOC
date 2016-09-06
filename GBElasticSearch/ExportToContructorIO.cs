using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GBDB;
using ConstructorIO;

namespace GBElasticSearch
{
   public class ExportToContructorIO
    {
        public void TransferDbListings()
        {

            var dbitems = new GBDB.GbItems();
            var items = dbitems.Get();


            int skip = 0;
            int threads = 1;
            var take = 1000;
            var count = items.Count();
            

            var client = new ConstructorIOAPI("U1ZeEcKZlWBnqvwhXBrd", "je0WEjmgY2SdixsjFHtR");
            var verified=client.Verify();
            while (skip < count)
            {



                var mydict = new Dictionary<string, string>();

                var dee = items.OrderBy(x => x.ItemID).Skip(skip).Take(take).Select(item => item).ToArray();
                var request = new List<List<ListItem>>();
                for (int i = 0; i < threads; i++)
                {
                    request.Add(
                        new List<ListItem>(dee.Skip(i * take / threads).Take(take / threads).Select(
                            x => 
                            new ListItem(
                               System.Web.HttpUtility.HtmlEncode(x.ItemTitle) ,
                                "Products",
                                x.ItemID.ToString(),
                                "",
                                @"http://www.gunbroker.com/item/"+x.ItemID,
                                null,
                                -1,
                                null
                                )
                                )));
                    
                }
                
                foreach (var itemList in request)
                {
                    
                    client.AddOrUpdateBatch(itemList,ListItemAutocompleteType.Products);
                }
                


                skip += take;
                Debug.WriteLine(skip);

            }



        }
    }
}
