using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadyBookAppV2.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public List<Stock> Stocks { get; set; }
 
    }
}
