using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDeal.Models
{
    public class User
    {
        public int id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}
