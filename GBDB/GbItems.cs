using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;


namespace GBDB
{
    internal static class MissingDllHack
    {
        private static SqlProviderServices instance = SqlProviderServices.Instance;
    }
    public class GbItems
    {
        public IQueryable<Item> Get()
        {

            var context = new GBDB.SampleGunBrokerItemsEntities();
            return context.Items;
        }
    }
}
