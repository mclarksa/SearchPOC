using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GBDB
{
    [MetadataType(typeof(Item))]
    [Nest.ElasticsearchType(IdProperty = "ItemID",Name ="listing")]
    
    public partial class Item
    {
        
        //public int ItemID { get; set; };
    }
}
