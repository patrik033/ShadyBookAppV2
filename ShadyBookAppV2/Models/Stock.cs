using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadyBookAppV2.Models
{
    public class Stock
    {

        public int StockItem { get; set; }
        
        public ulong BookId { get; set; }
        public Book Book { get; set; }


        public int StoreId { get; set; }
        public Store Store { get; set; }



    }
}
