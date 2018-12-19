using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDeal.Models
{
    public sealed class TestDeal
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string Pictures { get; set; }
        public int Owner { get; set; }
    }
}
