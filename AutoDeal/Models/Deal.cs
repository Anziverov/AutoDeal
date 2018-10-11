using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDeal.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
