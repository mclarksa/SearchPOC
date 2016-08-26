using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBElasticSearch
{
    class Program
    {
        static void Main(string[] args)
        {
           //var test = new ExportToElasticSearch();
            //test.TransferDbListings();
            //var constructorIO = new ExportToContructorIO();
            //constructorIO.TransferDbListings();
            var solr = new ExportToSOLR();
            solr.Export();
        }
    }
}
